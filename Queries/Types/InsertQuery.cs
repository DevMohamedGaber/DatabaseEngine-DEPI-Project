using DatabaseEngine.Core;
using DatabaseEngine.FileManager;
using DatabaseEngine.User;
using System.Data.Common;
using System.Dynamic;
using System.Reflection;

namespace DatabaseEngine.Queries.Types
{
    public class InsertQuery : Query
    {
        public InsertQuery(string[] stmt) : base(stmt) { }
        protected override void Execute()
        {
            if (statment.Count() < 2 || statment[1].ToLower() != "into")
            {
                Result.Errors.Add("Syntax Error: query is missing 'into' statment.");
                return;
            }
            if (statment.Count() < 3)
            {
                Result.Errors.Add("Syntax Error: No table name was given.");
                return;
            }

            string[] nameAndColumns = statment[2].Split('(');
            string tableName = nameAndColumns[0];

            _table = FileHandler.Read(tableName);

            if (_table == null)
            {
                Result.AddSyntaxError($"no table found with name '{tableName}'.");
                return;
            }

            // parse columns
            List<string>? columnNames = null;

            if(nameAndColumns.Length > 1)
            {
                string columnNamesString = nameAndColumns[1].Replace(")", "");
                columnNames = columnNamesString.Split(",").ToList();
            }

            if(columnNames != null)
            {
                foreach (string columnName in columnNames)
                {
                    if(_table.CheckColumnExists(columnName))
                    {
                        continue;
                    }

                    Result.AddProcessError($"table '{tableName}' doesn't have a column named '{columnName}'");
                    return;
                }
            }

            // parse values
            List<object>? Values = ParseValues();

            if(Values == null)
            {
                return;
            }
            if ((columnNames == null && _table.Columns.Count() != Values.Count()) || (columnNames != null && columnNames.Count() != Values.Count()))
            {
                Result.AddProcessError($"the number of values doesn't match the number of columns in '{tableName}' table.");
                return;
            }

            object[] record = new object[_table.Columns.Count()];

            /*
                columnNames = null => add to all columns
                columnNames != null => add only to specified columns
             */
            for (int i = 0; i < _table.Columns.Count(); i++)
            {
                if(columnNames == null)
                {
                    record[i] = Values[i];
                    continue;
                }

                int indexOfColumn = columnNames.IndexOf(_table.Columns[i].Name);
                if (indexOfColumn == -1)
                {
                    record[i] = null;
                    continue;
                }

                record[i] = Values[indexOfColumn];
            }

            _table.Rows.Add(record);

            _table.Save();

            UserHandler.SetSuccessMsg($"Insertion into table '{_table.Name}' Done Successfully");
        }
        private List<object>? ParseValues()
        {
            string valuesString = statment[3];
            if(!valuesString.StartsWith("values", true, null))
            {
                Result.AddSyntaxError("no values parametar were given.");
                return null;
            }
            valuesString = valuesString.Replace("values", "");

            if(!valuesString.StartsWith("(") || !valuesString.EndsWith(")"))
            {
                Result.Errors.Add("Syntax Error: values aren't defined correctly with (value1,value2,value3) Syntax.");
                return null;
            }

            valuesString = valuesString.TrimStart('(').TrimEnd(')');

            List<object> result = valuesString.Split(",").ToList<object>();

            return result;
        }
    }
}
