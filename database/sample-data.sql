-- Entirely fictional demonstration products, created for this repository.
-- Run after schema.sql, only in a development database used for demonstration.
USE mninventory;

INSERT INTO source_products (sku, name, qr, system_inventory)
SELECT 'DEMO-001', 'Demo Notebook', 'DEMO0001/DEMOALT0001', 12
WHERE NOT EXISTS (SELECT 1 FROM source_products WHERE sku = 'DEMO-001');

INSERT INTO source_products (sku, name, qr, system_inventory)
SELECT 'DEMO-002', 'Demo Storage Box', 'DEMO0002', 8
WHERE NOT EXISTS (SELECT 1 FROM source_products WHERE sku = 'DEMO-002');

INSERT INTO source_products (sku, name, qr, system_inventory)
SELECT 'DEMO-003', 'Demo Pen Set', 'DEMO0003', 5
WHERE NOT EXISTS (SELECT 1 FROM source_products WHERE sku = 'DEMO-003');
