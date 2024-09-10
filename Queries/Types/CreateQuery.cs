/*
    create table tableName (columnName columnType,columnName columnType,columnName columnType)
 */

using DatabaseEngine.FileManager;

namespace DatabaseEngine.Queries.Types
{
    public class CreateQuery : Query
    {
        public CreateQuery(string[] stmt) : base(stmt) { }

        public override void Execute()
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
            if(statment.Count() < 3)
            {
                Result.Errors.Add("Syntax Error: Create name hav");
            }
            FileStream file = FileHandler.Create(statment[2]);

            if (file == null)
            {
                Result.Errors.Add("Process Error: Table with the same name already exists");
                Success = false;
            }

            Console.WriteLine("Table ");
            Success = true;
        }
    }
}
