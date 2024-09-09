using System;
using System.IO;
using DatabaseEngine.Core;

namespace DatabaseEngine.FileManager
{
    public static class FileHandler
    {
        private static readonly string DB_LOCATION = "./DataStorage/";

        public static bool Exists(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return false;
            }

            return File.Exists(DB_LOCATION + tableName);
        }

        public static Table Read(string tableName)
        {
            Table table = new Table();

            return table;
        }

    }
}
