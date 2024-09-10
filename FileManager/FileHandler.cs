using System;
using System.IO;
using DatabaseEngine.Core;

namespace DatabaseEngine.FileManager
{
    public static class FileHandler
    {
        private static readonly string DB_LOCATION = "./DataStorage/";
        private static readonly string DB_EXTENTION = ".txt";

        public static bool Exists(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return false;
            }

            return File.Exists(GetFilePath(tableName));
        }
        public static FileStream Create(string tableName)
        {
            if (Exists(tableName))
            {
                return null;
            }

            return File.Create(GetFilePath(tableName));
        }

        public static Table Read(string tableName)
        {
            Table table = new Table();

            return table;
        }

        private static string GetFilePath(string fileName)
        {
            return Path.Combine(DB_LOCATION, fileName + DB_EXTENTION);
        }
    }
}
