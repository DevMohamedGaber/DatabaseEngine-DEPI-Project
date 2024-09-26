using DatabaseEngine.FileManager;
using DatabaseEngine.Queries;
using System.Reflection;
using System.Xml.Linq;

namespace DatabaseEngine.Core
{
    public class Table
    {
        public string Name { get; private set; }
        public List<Column> Columns { get; private set; }
        public List<object[]> Rows { get; private set; }

        public Table(string Name)
        {
            this.Name = Name;
            this.Columns = new List<Column>();
            this.Rows = new List<object[]>();
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
        public void AddRow(object[] row)
        {
            Rows.Add(row);
        }

        public void UpdateRow(int index, object[] row)
        {
            Rows[index] = row;
        }
        public bool Save(bool forceCreate = false)
        {
            return FileHandler.Write(this, forceCreate);
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

            foreach (object[] row in Rows)
            {
                string rowString = string.Join(QueryHelpers.rowSeparator, row);
                result.Add(rowString);
            }

            return result;
        }

        public bool CheckColumnExists(string columnName)
        {
            if(Columns.Count() == 0)
            {
                return false;
            }

            foreach (Column column in Columns)
            {
                if(column.Name == columnName)
                {
                    return true;
                }
            }

            return false;
        }

        public int GetColumnIndexByName(string name)
        {
            if(Columns.Count() == 0)
            {
                return -1;
            }

            for (int i = 0; i < Columns.Count(); i++)
            {
                if (Columns[i].Name == name)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
