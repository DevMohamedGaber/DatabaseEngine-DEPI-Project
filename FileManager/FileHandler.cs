using System;
using System.IO;
using System.Text;
using DatabaseEngine.Core;

// TODO: switch from normal file stream to stream writer
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
        public static bool Write(Table table, bool forceCreate = true)
        {
            // Cache file stream
            FileStream? file = null;

            // check if file exists, if not create it
            if (!Exists(table.Name))
            {
                if(!forceCreate)
                {
                    return false;
                }
                file = Create(table.Name);
            }

            if(file == null)
            {
                file = File.OpenWrite(GetFilePath(table.Name));
            }

            StringBuilder content = new StringBuilder();

            if(table.Columns.Count > 0)
            {
                content.AppendLine(table.GetColumnsString());
            }

            if(table.Rows.Count() > 0)
            {
                foreach (string row in table.GetRowsString())
                {
                    content.AppendLine(row);
                }
            }

            byte[] contentBytes = Encoding.ASCII.GetBytes(content.ToString());
            file.Write(contentBytes);

            file.Close();
            return true;
        }

        private static string GetFilePath(string fileName)
        {
            return Path.Combine(DB_LOCATION, fileName + DB_EXTENTION);
        }
    }
}
