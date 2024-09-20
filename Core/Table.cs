using DatabaseEngine.FileManager;
using System.Reflection;

namespace DatabaseEngine.Core
{
    public class Table
    {
        public string Name { get; private set; }
        public List<Column> Columns { get; private set; }
        public List<object> Rows { get; private set; }

        public Table()
        {
            this.Name = string.Empty;
            this.Columns = new List<Column>();
            this.Rows = new List<object>();
        }

        public bool SetName(string name)
        {
            if(FileHandler.Exists(name))
            {
                return false;
            }

            this.Name = name;

            return true;
        }

        public bool AddColumn(Column column)
        {
            if(Columns.Contains(column))
            {
                return false;
            }

            Columns.Add(column);
            return true;
        }

        public bool Save()
        {
            return FileHandler.Write(this);
        }

        public string GetColumnsString()
        {
            string result = string.Empty;

            for (int i = 0; i < Columns.Count(); i++)
            {
                result += Columns[i].ToString();

                if(i != Columns.Count() - 1)
                {
                    result += ',';
                }
            }

            return result;
        }
        public List<string>? GetRowsString()
        {
            if (Rows.Count() == 0)
            {
                return null;
            }

            List<string> result = new List<string>();

            foreach (object row in Rows)
            {
                PropertyInfo[] props = row.GetType().GetProperties();

                if(props.Length == 0)
                {
                    continue;
                }

                string rowString = "";
                foreach (PropertyInfo prop in props)
                {
                    rowString += prop.GetValue(row).ToString();
                }

                result.Add(rowString);
            }

            return result;
        }
    }
}
