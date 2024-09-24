using System;
using System.IO;
using System.Text;
using DatabaseEngine.Core;
using DatabaseEngine.Queries;

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
        public static bool Delete(string tableName)
        {
            if (!Exists(tableName))
            {
                return false;
            }

            File.Delete(GetFilePath(tableName));

            return true;
        }

        public static Table? Read(string tableName)
        {
            // check if file exists, if not create it
            if (!Exists(tableName))
            {
                return null;
            }
            List<string> lines = File.ReadLines(GetFilePath(tableName)).ToList();
            Table table = new Table(tableName);
            
            // define columns
            string[] columns = lines[0].Split(",");

            foreach (var column in columns)
            {
                string[] col = column.Split(" ");
                table.AddColumn(new Column(col));
            }

            // define rows
            for (int i = 2; i < lines.Count(); i++)
            {
                string[] values = lines[i].Split(QueryHelpers.rowSeparator);
                table.AddRow(values);
            }

            return table;
        }
        public static bool Write(Table table, bool forceCreate)
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


            StringBuilder content = new StringBuilder();

            if(table.Columns.Count > 0)
            {
                content.AppendLine(table.GetColumnsString());
            }

            content.AppendLine();

            if (table.Rows.Count() > 0)
            {
                foreach (string row in table.GetRowsString())
                {
                    content.AppendLine(row);
                }
            }

            byte[] contentBytes = Encoding.ASCII.GetBytes(content.ToString());

            if (!forceCreate)
            {
                using (FileStream fs = new FileStream(GetFilePath(table.Name), FileMode.Truncate, FileAccess.Write))
                {
                    fs.Close();
                };
            }

            if (file == null)
            {
                file = File.OpenWrite(GetFilePath(table.Name));
            }
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
