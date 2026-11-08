using MNInventoryApp.Data;
using MNInventoryApp.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MNInventoryApp.Services
{
    public class MySqlInventoryService
    {
        public bool SourceProductsExist()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM source_products;";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public List<Product> LoadSourceProducts(bool activeOnly = true)
        {
            List<Product> products = new List<Product>();

            using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();

                string query = @"
                    SELECT id, sku, name, qr, system_inventory, is_active
                    FROM source_products
                    " + (activeOnly ? "WHERE is_active = 1" : "") + @"
                    ORDER BY id;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            SKU = reader["sku"].ToString(),
                            Name = reader["name"].ToString(),
                            QR = reader["qr"].ToString(),
                            SystemInventory = Convert.ToInt32(reader["system_inventory"]),
                            Counted = 0,
                            IsActive = Convert.ToBoolean(reader["is_active"])
                        });
                    }
                }
            }

            return products;
        }

        public List<Product> LoadScannedProducts(bool activeOnly = true)
        {
            List<Product> products = new List<Product>();

            using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();

                string query = @"
                    SELECT
                        sp.id,
                        sp.sku,
                        sp.name,
                        sp.qr,
                        sp.system_inventory,
                        sp.is_active,
                        sc.counted
                    FROM scanned_products sc
                    INNER JOIN source_products sp
                        ON sc.source_product_id = sp.id
                    " + (activeOnly ? "WHERE sp.is_active = 1" : "") + @"
                    ORDER BY sp.id;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            SKU = reader["sku"].ToString(),
                            Name = reader["name"].ToString(),
                            QR = reader["qr"].ToString(),
                            SystemInventory = Convert.ToInt32(reader["system_inventory"]),
                            Counted = Convert.ToInt32(reader["counted"]),
                            IsActive = Convert.ToBoolean(reader["is_active"])
                        });
                    }
                }
            }

            return products;
        }

        /// <summary>
        /// Distinct <c>machine_name</c> values present in <c>scan_history</c> (for per-PC filters and bulk undo).
        /// </summary>
        public List<string> GetDistinctScanHistoryMachineNames()
        {
            List<string> names = new List<string>();

            using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();

                string query = @"
                    SELECT DISTINCT machine_name
                    FROM scan_history
                    WHERE machine_name IS NOT NULL AND TRIM(machine_name) <> ''
                    ORDER BY machine_name;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string n = reader["machine_name"]?.ToString()?.Trim();
                        if (!string.IsNullOrEmpty(n))
                            names.Add(n);
                    }
                }
            }

            return names;
        }

        /// <summary>
        /// Scanned products with <see cref="Product.Counted"/> equal to net units from <c>scan_history</c>
        /// for <paramref name="machineName"/> (SCAN adds, UNDO subtracts). Excludes products with net ≤ 0.
        /// </summary>
        public List<Product> LoadScannedProductsNetByMachine(string machineName, bool activeOnly = true)
        {
            if (string.IsNullOrWhiteSpace(machineName))
                return LoadScannedProducts(activeOnly);

            List<Product> products = new List<Product>();

            using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();

                string query = @"
                    SELECT
                        sp.id,
                        sp.sku,
                        sp.name,
                        sp.qr,
                        sp.system_inventory,
                        sp.is_active,
                        net_tab.net_qty AS counted
                    FROM (
                        SELECT
                            source_product_id,
                            SUM(CASE
                                WHEN UPPER(IFNULL(action_type, '')) = 'SCAN' THEN quantity_added
                                WHEN UPPER(IFNULL(action_type, '')) = 'UNDO' THEN -quantity_added
                                ELSE 0
                            END) AS net_qty
                        FROM scan_history
                        WHERE machine_name = @machine
                        GROUP BY source_product_id
                        HAVING net_qty > 0
                    ) net_tab
                    INNER JOIN source_products sp ON sp.id = net_tab.source_product_id
                    " + (activeOnly ? "WHERE sp.is_active = 1" : "") + @"
                    ORDER BY sp.id;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@machine", machineName.Trim());

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                SKU = reader["sku"].ToString(),
                                Name = reader["name"].ToString(),
                                QR = reader["qr"].ToString(),
                                SystemInventory = Convert.ToInt32(reader["system_inventory"]),
                                Counted = Convert.ToInt32(reader["counted"]),
                                IsActive = Convert.ToBoolean(reader["is_active"])
                            });
                        }
                    }
                }
            }

            return products;
        }

        /// <summary>
        /// All SCAN contributions for <paramref name="machineName"/>, aggregated per product, for bulk undo.
        /// </summary>
        public List<(int SourceProductId, int Quantity, string Sku, string Name, string Qr)> GetAggregatedScanUndosForMachine(
            string machineName)
        {
            if (string.IsNullOrWhiteSpace(machineName))
                return new List<(int, int, string, string, string)>();

            List<(int SourceProductId, int Quantity, string Sku, string Name, string Qr)> rows =
                new List<(int, int, string, string, string)>();

            using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();

                string query = @"
                    SELECT
                        sh.source_product_id,
                        CAST(SUM(sh.quantity_added) AS SIGNED) AS qty,
                        MAX(sp.sku) AS sku,
                        MAX(sp.name) AS name,
                        MAX(sp.qr) AS qr
                    FROM scan_history sh
                    INNER JOIN source_products sp ON sp.id = sh.source_product_id
                    WHERE sh.machine_name = @machine
                      AND UPPER(IFNULL(sh.action_type, '')) = 'SCAN'
                    GROUP BY sh.source_product_id
                    HAVING SUM(sh.quantity_added) > 0;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@machine", machineName.Trim());

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rows.Add((
                                Convert.ToInt32(reader["source_product_id"]),
                                Convert.ToInt32(reader["qty"]),
                                reader["sku"]?.ToString() ?? string.Empty,
                                reader["name"]?.ToString() ?? string.Empty,
                                reader["qr"]?.ToString() ?? string.Empty));
                        }
                    }
                }
            }

            return rows;
        }

        /// <summary>
        /// Returns true if <paramref name="barcode"/> matches any <c>source_products.qr</c> value
        /// as a full string or as one slash-separated segment (same matching rules as scanning).
        /// </summary>
        /// <remarks>
        /// This is stricter than a plain SQL <c>LIKE '%barcode%'</c> substring match, so codes are not
        /// blocked incorrectly when they appear only inside a longer token.
        /// </remarks>
        public bool SourceProductBarcodeExists(string barcode)
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();
                return SourceProductBarcodeExists(barcode, connection, null);
            }
        }

        /// <summary>
        /// Inserts a new active row into <c>source_products</c> for store-created items missing from import files.
        /// </summary>
        /// <returns>New <c>source_products.id</c>.</returns>
        /// <exception cref="ArgumentException">Name or barcode empty, or invalid system inventory.</exception>
        /// <exception cref="InvalidOperationException">Barcode already exists in source inventory.</exception>
        public int AddManualSourceProduct(string name, string sku, string qr, int systemInventory)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required.", nameof(name));

            if (string.IsNullOrWhiteSpace(qr))
                throw new ArgumentException("Barcode / QR is required.", nameof(qr));

            if (systemInventory < 0)
                throw new ArgumentOutOfRangeException(nameof(systemInventory), "System inventory cannot be negative.");

            string cleanName = name.Trim();
            string cleanQr = qr.Trim();
            string cleanSku = string.IsNullOrWhiteSpace(sku) ? string.Empty : sku.Trim();

            using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        if (SourceProductBarcodeExists(cleanQr, connection, transaction))
                        {
                            throw new InvalidOperationException(
                                "This barcode is already in source inventory. Each barcode must be unique.");
                        }

                        const string insertQuery = @"
                            INSERT INTO source_products (sku, name, qr, system_inventory, imported_at, is_active)
                            VALUES (@sku, @name, @qr, @system_inventory, NOW(), 1);";

                        using (MySqlCommand cmd = new MySqlCommand(insertQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@sku", cleanSku);
                            cmd.Parameters.AddWithValue("@name", cleanName);
                            cmd.Parameters.AddWithValue("@qr", cleanQr);
                            cmd.Parameters.AddWithValue("@system_inventory", systemInventory);
                            cmd.ExecuteNonQuery();
                        }

                        int newId;
                        using (MySqlCommand idCmd = new MySqlCommand("SELECT LAST_INSERT_ID();", connection, transaction))
                        {
                            newId = Convert.ToInt32(idCmd.ExecuteScalar());
                        }

                        transaction.Commit();
                        return newId;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private static bool SourceProductBarcodeExists(string barcode, MySqlConnection connection, MySqlTransaction transaction)
        {
            if (string.IsNullOrWhiteSpace(barcode))
                return false;

            string input = barcode.Trim();

            using (MySqlCommand command = new MySqlCommand("SELECT qr FROM source_products;", connection, transaction))
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader["qr"] == DBNull.Value)
                        continue;

                    string qr = reader["qr"].ToString();
                    if (string.IsNullOrWhiteSpace(qr))
                        continue;

                    foreach (string segment in qr.Split('/'))
                    {
                        if (string.Equals(segment.Trim(), input, StringComparison.Ordinal))
                            return true;
                    }
                }
            }

            return false;
        }

        public List<ScanHistoryItem> LoadScanHistory(int maxRows = 8000)
        {
            List<ScanHistoryItem> history = new List<ScanHistoryItem>();

            using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();

                string query = @"
                    SELECT
                        sh.source_product_id,
                        sp.name,
                        sp.sku,
                        sp.qr,
                        sh.scanned_code,
                        sh.quantity_added,
                        sh.action_type,
                        sh.machine_name,
                        sh.created_at
                    FROM scan_history sh
                    INNER JOIN source_products sp
                        ON sh.source_product_id = sp.id
                    ORDER BY sh.created_at DESC
                    LIMIT @maxRows;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@maxRows", maxRows);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            history.Add(new ScanHistoryItem
                            {
                                SourceProductId = Convert.ToInt32(reader["source_product_id"]),
                                Name = reader["name"].ToString(),
                                SKU = reader["sku"].ToString(),
                                QR = reader["qr"].ToString(),
                                ScannedCode = reader["scanned_code"].ToString(),
                                QuantityAdded = Convert.ToInt32(reader["quantity_added"]),
                                ActionType = reader["action_type"].ToString(),
                                MachineName = reader["machine_name"].ToString(),
                                ScanTime = Convert.ToDateTime(reader["created_at"])
                            });
                        }
                    }
                }
            }

            return history;
        }

        public void AddOrUpdateScannedProduct(Product product, int quantity, string scannedCode)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0.");

            using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int sourceId = product.Id;

                        if (sourceId <= 0)
                        {
                            sourceId = GetSourceProductIdBySku(connection, transaction, product.SKU);
                        }

                        if (sourceId <= 0)
                            throw new Exception("Source product not found.");

                        int existingCount = GetExistingCountedQuantity(connection, transaction, sourceId);

                        if (existingCount > 0)
                        {
                            string updateQuery = @"
                                UPDATE scanned_products
                                SET counted = counted + @qty,
                                    last_updated_at = NOW()
                                WHERE source_product_id = @id;";

                            using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@qty", quantity);
                                cmd.Parameters.AddWithValue("@id", sourceId);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string insertQuery = @"
                                INSERT INTO scanned_products (source_product_id, counted, last_updated_at)
                                VALUES (@id, @qty, NOW());";

                            using (MySqlCommand cmd = new MySqlCommand(insertQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@id", sourceId);
                                cmd.Parameters.AddWithValue("@qty", quantity);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        string historyQuery = @"
                            INSERT INTO scan_history
                            (source_product_id, scanned_code, quantity_added, action_type, machine_name)
                            VALUES (@id, @code, @qty, 'SCAN', @machine);";

                        using (MySqlCommand cmd = new MySqlCommand(historyQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@id", sourceId);
                            cmd.Parameters.AddWithValue("@code", scannedCode ?? product.QR ?? "");
                            cmd.Parameters.AddWithValue("@qty", quantity);
                            cmd.Parameters.AddWithValue("@machine", Environment.MachineName);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void UndoQuantity(Product product, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (quantity <= 0)
                throw new ArgumentException("Undo quantity must be greater than 0.");

            using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int sourceId = product.Id;

                        if (sourceId <= 0)
                        {
                            sourceId = GetSourceProductIdBySku(connection, transaction, product.SKU);
                        }

                        if (sourceId <= 0)
                            throw new Exception("Source product not found for undo.");

                        string updateQuery = @"
                            UPDATE scanned_products
                            SET counted = GREATEST(counted - @qty, 0),
                                last_updated_at = NOW()
                            WHERE source_product_id = @id;";

                        using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@qty", quantity);
                            cmd.Parameters.AddWithValue("@id", sourceId);
                            cmd.ExecuteNonQuery();
                        }

                        string deleteZeroQuery = @"
                            DELETE FROM scanned_products
                            WHERE source_product_id = @id
                              AND counted <= 0;";

                        using (MySqlCommand cmd = new MySqlCommand(deleteZeroQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@id", sourceId);
                            cmd.ExecuteNonQuery();
                        }

                        string historyQuery = @"
                            INSERT INTO scan_history
                            (source_product_id, scanned_code, quantity_added, action_type, machine_name)
                            VALUES (@id, @code, @qty, 'UNDO', @machine);";

                        using (MySqlCommand cmd = new MySqlCommand(historyQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@id", sourceId);
                            cmd.Parameters.AddWithValue("@code", product.QR ?? "");
                            cmd.Parameters.AddWithValue("@qty", quantity);
                            cmd.Parameters.AddWithValue("@machine", Environment.MachineName);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void SyncSourceProductsPreserveCounts(List<Product> newProducts)
        {
            if (newProducts == null)
                throw new ArgumentNullException(nameof(newProducts));

            using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string deactivateQuery = "UPDATE source_products SET is_active = 0;";
                        using (MySqlCommand cmd = new MySqlCommand(deactivateQuery, connection, transaction))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        List<Product> existingProducts = LoadSourceProductsForSync(connection, transaction);

                        foreach (Product newProduct in newProducts)
                        {
                            Product matchedProduct = FindMatchingExistingProduct(existingProducts, newProduct);

                            if (matchedProduct != null)
                            {
                                string updateQuery = @"
                                    UPDATE source_products
                                    SET sku = @sku,
                                        name = @name,
                                        qr = @qr,
                                        system_inventory = @system_inventory,
                                        is_active = 1,
                                        imported_at = NOW()
                                    WHERE id = @id;";

                                using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@sku", newProduct.SKU);
                                    cmd.Parameters.AddWithValue("@name", newProduct.Name);
                                    cmd.Parameters.AddWithValue("@qr", newProduct.QR);
                                    cmd.Parameters.AddWithValue("@system_inventory", newProduct.SystemInventory);
                                    cmd.Parameters.AddWithValue("@id", matchedProduct.Id);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string insertQuery = @"
                                    INSERT INTO source_products
                                    (sku, name, qr, system_inventory, imported_at, is_active)
                                    VALUES (@sku, @name, @qr, @system_inventory, NOW(), 1);";

                                using (MySqlCommand cmd = new MySqlCommand(insertQuery, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@sku", newProduct.SKU);
                                    cmd.Parameters.AddWithValue("@name", newProduct.Name);
                                    cmd.Parameters.AddWithValue("@qr", newProduct.QR);
                                    cmd.Parameters.AddWithValue("@system_inventory", newProduct.SystemInventory);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // Only for first setup / dangerous reset
        public void ReplaceSourceProducts(List<Product> products)
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));

            using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string deleteHistoryQuery = "DELETE FROM scan_history;";
                        using (MySqlCommand cmd = new MySqlCommand(deleteHistoryQuery, connection, transaction))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        string deleteScannedQuery = "DELETE FROM scanned_products;";
                        using (MySqlCommand cmd = new MySqlCommand(deleteScannedQuery, connection, transaction))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        string deleteSourceQuery = "DELETE FROM source_products;";
                        using (MySqlCommand cmd = new MySqlCommand(deleteSourceQuery, connection, transaction))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        string insertQuery = @"
                            INSERT INTO source_products
                            (sku, name, qr, system_inventory, imported_at, is_active)
                            VALUES (@sku, @name, @qr, @system_inventory, NOW(), 1);";

                        foreach (Product product in products)
                        {
                            using (MySqlCommand cmd = new MySqlCommand(insertQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@sku", product.SKU);
                                cmd.Parameters.AddWithValue("@name", product.Name);
                                cmd.Parameters.AddWithValue("@qr", product.QR);
                                cmd.Parameters.AddWithValue("@system_inventory", product.SystemInventory);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // Only for old migration / manual restore
        public void ReplaceScannedProductsFromExcel(List<Product> importedScannedProducts)
        {
            if (importedScannedProducts == null)
                throw new ArgumentNullException(nameof(importedScannedProducts));

            using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string deleteHistoryQuery = "DELETE FROM scan_history;";
                        using (MySqlCommand cmd = new MySqlCommand(deleteHistoryQuery, connection, transaction))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        string deleteScannedQuery = "DELETE FROM scanned_products;";
                        using (MySqlCommand cmd = new MySqlCommand(deleteScannedQuery, connection, transaction))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        List<Product> sourceProducts = LoadSourceProductsForSync(connection, transaction);

                        foreach (Product importedProduct in importedScannedProducts)
                        {
                            if (importedProduct.Counted <= 0)
                                continue;

                            Product matchedProduct = FindMatchingExistingProduct(sourceProducts, importedProduct);

                            if (matchedProduct == null)
                                continue;

                            string insertScannedQuery = @"
                                INSERT INTO scanned_products (source_product_id, counted, last_updated_at)
                                VALUES (@source_product_id, @counted, NOW());";

                            using (MySqlCommand cmd = new MySqlCommand(insertScannedQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@source_product_id", matchedProduct.Id);
                                cmd.Parameters.AddWithValue("@counted", importedProduct.Counted);
                                cmd.ExecuteNonQuery();
                            }

                            string insertHistoryQuery = @"
                                INSERT INTO scan_history
                                (source_product_id, scanned_code, quantity_added, action_type, machine_name)
                                VALUES (@source_product_id, @scanned_code, @quantity_added, 'IMPORT', @machine_name);";

                            using (MySqlCommand cmd = new MySqlCommand(insertHistoryQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@source_product_id", matchedProduct.Id);
                                cmd.Parameters.AddWithValue("@scanned_code", importedProduct.QR ?? "");
                                cmd.Parameters.AddWithValue("@quantity_added", importedProduct.Counted);
                                cmd.Parameters.AddWithValue("@machine_name", Environment.MachineName);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void ClearCurrentCountSession()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string deleteHistoryQuery = "DELETE FROM scan_history;";
                        using (MySqlCommand cmd = new MySqlCommand(deleteHistoryQuery, connection, transaction))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        string deleteScannedQuery = "DELETE FROM scanned_products;";
                        using (MySqlCommand cmd = new MySqlCommand(deleteScannedQuery, connection, transaction))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private int GetSourceProductIdBySku(MySqlConnection connection, MySqlTransaction transaction, string sku)
        {
            string query = @"
                SELECT id
                FROM source_products
                WHERE sku = @sku
                LIMIT 1;";

            using (MySqlCommand cmd = new MySqlCommand(query, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@sku", sku ?? "");

                object result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                    return 0;

                return Convert.ToInt32(result);
            }
        }

        private int GetExistingCountedQuantity(MySqlConnection connection, MySqlTransaction transaction, int sourceId)
        {
            string query = @"
                SELECT counted
                FROM scanned_products
                WHERE source_product_id = @id
                LIMIT 1;";

            using (MySqlCommand cmd = new MySqlCommand(query, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@id", sourceId);

                object result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                    return 0;

                return Convert.ToInt32(result);
            }
        }

        private List<Product> LoadSourceProductsForSync(MySqlConnection connection, MySqlTransaction transaction)
        {
            List<Product> products = new List<Product>();

            string query = @"
                SELECT id, sku, name, qr, system_inventory, is_active
                FROM source_products
                ORDER BY id;";

            using (MySqlCommand command = new MySqlCommand(query, connection, transaction))
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        SKU = reader["sku"].ToString(),
                        Name = reader["name"].ToString(),
                        QR = reader["qr"].ToString(),
                        SystemInventory = Convert.ToInt32(reader["system_inventory"]),
                        Counted = 0,
                        IsActive = Convert.ToBoolean(reader["is_active"])
                    });
                }
            }

            return products;
        }

        private Product FindMatchingExistingProduct(List<Product> existingProducts, Product newProduct)
        {
            if (existingProducts == null || newProduct == null)
                return null;

            if (!string.IsNullOrWhiteSpace(newProduct.SKU))
            {
                foreach (Product existing in existingProducts)
                {
                    if (string.Equals(existing.SKU, newProduct.SKU, StringComparison.OrdinalIgnoreCase))
                    {
                        return existing;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(newProduct.QR))
            {
                string[] newCodes = newProduct.QR.Split('/');

                foreach (Product existing in existingProducts)
                {
                    if (string.IsNullOrWhiteSpace(existing.QR))
                        continue;

                    string[] existingCodes = existing.QR.Split('/');

                    foreach (string newCode in newCodes)
                    {
                        string cleanNewCode = newCode.Trim();
                        if (string.IsNullOrWhiteSpace(cleanNewCode))
                            continue;

                        foreach (string existingCode in existingCodes)
                        {
                            string cleanExistingCode = existingCode.Trim();
                            if (string.IsNullOrWhiteSpace(cleanExistingCode))
                                continue;

                            if (cleanNewCode == cleanExistingCode)
                            {
                                return existing;
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
