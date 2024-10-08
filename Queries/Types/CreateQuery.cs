﻿/*
    create table tableName (columnName columnType,columnName columnType,columnName columnType)
 */

using DatabaseEngine.Core;
using DatabaseEngine.FileManager;
using DatabaseEngine.User;

namespace DatabaseEngine.Queries.Types
{
    public class CreateQuery : Query
    {
        public CreateQuery(string[] stmt) : base(stmt) { }

        protected override void Execute()
        {
            if(statment.Count() < 2)
            {
                Result.Errors.Add("Syntax Error: no type specified to create such as table");
                return;
            }

            string typeKeyWord = statment[1].ToLower();
            if (typeKeyWord == "table")
            {
                CreateTable();
            }

            // open for other things to create
        }

        private void CreateTable()
        {
            Table? table = ParseTable();

            if(table == null)
            {
                return;
            }

            table.Save(true);

            UserHandler.SetSuccessMsg($"Table {table.Name} created successfully");
            Success = true;
        }

        private Table? ParseTable()
        {
            Table table = new Table(string.Empty);

            if(statment.Count() < 3)
            {
                Result.AddSyntaxError("No table name was given");
                return null;
            }

            string tableName = statment[2];

            if(!QueryHelpers.IsValidTableName(tableName))
            {
                Result.AddProcessError("invalid table name");
                return null;
            }

            if (!table.SetName(tableName))
            {
                Result.AddProcessError("Table with the same name already exists");
                return null;
            }
            // (columnName columnType,columnName columnType,columnName columnType)
            if (statment.Count() < 4)
            {
                Result.AddSyntaxError("No columns detected");
                return null;
            }
            string rawColumns = string.Empty;

            for (int i = 3; i < statment.Count(); i++)
            {
                if (statment[i] == ",")
                {
                    continue;
                }

                rawColumns += statment[i].Replace(" ", "");

                if(i != statment.Count() - 1 && !statment[i].EndsWith(","))
                {
                    rawColumns += " ";
                }
            }

            if (!rawColumns.StartsWith('(') && !rawColumns.EndsWith(')'))
            {
                Result.Errors.Add("Syntax Error: Columns aren't defined correctly with (Name Type) Syntax.");
                return null;
            }
            rawColumns = rawColumns.TrimStart('(').TrimEnd(')');

            string[] columnsString = rawColumns.Split(',');

            if(columnsString.Length == 0)
            {
                Result.Errors.Add("Syntax Error: No columns were given.");
                return null;
            }

            foreach (string columnString in columnsString)
            {
                string[] columnArr = columnString.Split(' ');

                if (columnArr.Length < 2)
                {
                    Result.Errors.Add($"Syntax Error: column isn't corrrectly defined around >...{columnString} ...<");
                    return null;
                }
                Column column = new Column(columnArr);

                if(!table.AddColumn(column))
                {
                    Result.Errors.Add($"Syntax Error: a column with name {column.Name} already exists.");
                    return null;
                }
            }

            return table;
        }
    }
}
