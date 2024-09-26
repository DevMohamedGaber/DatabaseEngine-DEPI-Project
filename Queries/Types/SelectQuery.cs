using DatabaseEngine.Core;
using DatabaseEngine.FileManager;
using System;

namespace DatabaseEngine.Queries.Types
{
    public class SelectQuery : Query
    {
        public SelectQuery(string[] stmt) : base(stmt) { }
        protected override void Execute()
        {
            if(statment.Count() < 2)
            {
                Result.AddSyntaxError("no columns were givien");
                return;
            }

            string columnsString = statment[1];
            string[] columnNames = null;

            if(columnsString != "*" && columnsString.ToLower() != "all")
            {
                columnNames = columnsString.Split(',');
            }

            if(statment.Count() < 3 || statment[2].ToLower() != "from")
            {
                Result.AddSyntaxError("no 'from' keyword after table name found");
                return;
            }

            if(statment.Count() < 4)
            {
                Result.AddSyntaxError("no table name given");
                return;
            }

            _table = FileHandler.Read(statment[3]);

            if(_table == null)
            {
                Result.AddProcessError($"no table was found named '{statment[3]}'");
                return;
            }

            List<int> columnIndexes = null;
            if (columnNames != null)
            {
                columnIndexes = new List<int>();

                foreach (string colName in columnNames)
                {
                    int index = _table.GetColumnIndexByName(colName);
                    if (index == -1)
                    {
                        Result.AddProcessError($"no column named '{colName}' in table '{_table.Name}'");
                        return;
                    }
                    columnIndexes.Add(index);
                }
            }

            if (!ParseConditions() && Result.Errors.Count() > 0)
            {
                return;
            }

            // select all
            if (conditions.Count() == 0)
            {
                ShowTable(columnIndexes, _table.Rows);
                return;
            }

            List<object[]> selectedRows = new List<object[]>();

            foreach (object[] row in _table.Rows)
            {
                foreach (Condition condition in conditions)
                {
                    if(condition.Validate(row, ref _table))
                    {
                        selectedRows.Add(row);
                    }
                }
            }

            ShowTable(columnIndexes, selectedRows);
        }

        private void ShowTable(List<int> columnIndexes, List<object[]> rows)
        {
            string colsRow = "|";
            string rowsString = string.Empty;
            int widestLineCount = 0;
            string line = "";

            // columns line
            if(columnIndexes == null || columnIndexes.Count() == 0)
            {
                foreach (Column col in _table.Columns)
                {
                    colsRow += $" {col.Name} |";
                }
            }
            else
            {
                foreach (int index in columnIndexes)
                {
                    colsRow += $" {_table.Columns[index].Name} |";
                }
            }
            colsRow += "\n";
            widestLineCount = colsRow.Length;

            // row lines
            foreach (object[] row in rows)
            {
                string rowString = "|";

                // if select all
                if(columnIndexes == null || columnIndexes.Count() == 0)
                {
                    foreach (string cell in row)
                    {
                        rowString += $" {cell} |";
                    }
                }
                // if select specific columns
                else
                {
                    foreach (int colIndex in columnIndexes)
                    {
                        rowString += $" {row[colIndex]} |";
                    }
                }
                rowString += "\n";

                // check the widest line
                if (rowString.Length > widestLineCount)
                {
                    widestLineCount = rowString.Length;
                }

                rowsString += rowString;
            }

            // line
            for (int i = 0; i < widestLineCount; i++)
            {
                line += "-";
            }
            line += "\n";
            // print final result
            string finalResult = line + colsRow + line + rowsString + line;
            Console.WriteLine(finalResult);
        }
    }
}
