# Local setup

## Prerequisites

- Windows for the Windows Forms desktop application.
- .NET 10 SDK, verified with `dotnet --version`.
- A MySQL server and a client such as MySQL Shell, the `mysql` command-line client, or MySQL Workbench.
- Network access to NuGet for the initial package restore.

The core console project targets `net10.0`; the desktop project targets `net10.0-windows`. The full solution should be built on Windows.

## Create a demonstration database

Open a MySQL session using a local account permitted to create a database and tables. Run the repository's SQL scripts. With the `mysql` client, use forward slashes in the paths:

```sql
SOURCE C:/path/to/MNInventory/database/schema.sql;
SOURCE C:/path/to/MNInventory/database/sample-data.sql;
```

The schema creates `mninventory` and the `source_products`, `scanned_products`, and `scan_history` tables. The second script is optional and contains fictional products. Read a script before applying it to an existing database; use a separate demonstration database for evaluation.

The application does not create tables at startup. Provision the schema first, then create an application database account using your own password. Its runtime operations require `SELECT`, `INSERT`, `UPDATE`, and `DELETE` on the application database.

## Supply the connection string

The desktop app reads the `MNINVENTORY_CONNECTION_STRING` process environment variable. It has no embedded connection string or password, and it does not automatically read a `.env` file.

Connection-string format, with placeholders to replace locally:

```text
Server=localhost;Port=3306;Database=mninventory;Uid=<your-user>;Pwd=<your-password>;
```

Set it in the PowerShell session that launches the app:

```powershell
$env:MNINVENTORY_CONNECTION_STRING = Read-Host "Enter your local MySQL connection string"
```

An app launched from Visual Studio must receive the variable through the environment of that IDE process or an appropriate local debug configuration. Keep credentials out of committed project files. Multiple desktop clients can use the same MySQL database; each client needs its own connection configuration and network access to the server.

## Build and launch

From the repository root:

```powershell
dotnet restore MNInventory.slnx
dotnet build MNInventory.slnx -c Release --no-restore
dotnet run --project src/MNInventoryUI/MNInventoryUI.csproj -c Release --no-build
```

You can also open `MNInventory.slnx` in a compatible Visual Studio installation and select `MNInventoryUI` as the startup project.

With an empty connected database, choose **Import New Inventory** and select a workbook matching [the input format](workbook-format.md). With the fictional SQL sample loaded, scan or type one of those sample barcodes immediately.

Counted-inventory workbooks, unscanned reports, and history CSV files are written to the current Windows user's Desktop. Barcode-label PDF and PNG export dialogs let you select the destination.

## Workbook-only console mode

This workflow stores counts in a local workbook and does not use the MySQL connection variable:

```powershell
dotnet run --project src/MNInventoryApp/MNInventoryApp.csproj -- --help
dotnet run --project src/MNInventoryApp/MNInventoryApp.csproj -- "C:\path\source.xlsx" "C:\path\counts.xlsx"
```

Use a source workbook with the documented headers. The count workbook may be a previous export; when it does not exist, the tool starts with no counted products. Scan or type a barcode, enter a positive quantity, and use `exit` to finish. Successful count updates save the count workbook.

## Troubleshooting

| Symptom | Check |
| --- | --- |
| Connection variable is missing | Set it in the process environment that launches the desktop app. |
| MySQL connection fails | Confirm the server is running, the host/port is reachable, and the account can access `mninventory`. |
| Table does not exist | Apply `database/schema.sql` to the database named in the connection string. |
| Workbook import fails or returns no products | Use `.xlsx`, check the first worksheet's header row, and fill SKU, Name, and Barcode. |
| Barcode is not found | Import the product first; check whitespace, leading zeroes, and conflicting barcode assignments. |
| Counts changed on another computer | Reopen the relevant view or reload the application to retrieve current database state. |
| Desktop project cannot build | Verify Windows and .NET 10 SDK are available, then restore packages. |

The application uses a shared count session. **Start New Count** clears counted rows and history for every client using that database. Preserve reports before resetting a session.
