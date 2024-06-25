namespace DataPersistency.Queries
{
    public class SeedDataQueryInSQLite
    {
        public const string query =
            @"CREATE TABLE Candidates (
              Id INTEGER PRIMARY KEY AUTOINCREMENT,
              FirstName TEXT NOT NULL,
              LastName TEXT NOT NULL,
              Email TEXT NOT NULL UNIQUE,
              Mobile TEXT,
              DegreeId INTEGER,
              CV BLOB, -- Add a BLOB column to store CV data (if needed)
              CreationTime DATETIME DEFAULT CURRENT_TIMESTAMP,
              FOREIGN KEY (DegreeId) REFERENCES Degrees(Id)
            );

            CREATE TABLE Degrees (
              Id INTEGER PRIMARY KEY AUTOINCREMENT,
              Name TEXT NOT NULL,
              CreationTime DATETIME DEFAULT CURRENT_TIMESTAMP
            );";
    }
}
