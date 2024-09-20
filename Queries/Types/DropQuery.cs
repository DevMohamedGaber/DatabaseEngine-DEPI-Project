using DatabaseEngine.Core;
using DatabaseEngine.FileManager;
using DatabaseEngine.User;

namespace DatabaseEngine.Queries.Types
{
    public class DropQuery : Query
    {
        public DropQuery(string[] stmt) : base(stmt) { }
        protected override void Execute()
        {
            if (statment.Count() < 2)
            {
                Result.Errors.Add("Syntax Error: no type specified to drop such as table.");
                return;
            }

            string typeKeyWord = statment[1].ToLower();
            if (typeKeyWord == "table")
            {
                DropTable();
            }

            // open for other things to drop
        }

        private void DropTable()
        {
            if (statment.Count() < 3)
            {
                Result.Errors.Add("Syntax Error: no table name were given.");
                return;
            }

            string tableName = statment[2];
            if(!FileHandler.Delete(tableName))
            {
                Result.Errors.Add("Process Error: no table found with this name");
                return;
            }

            UserHandler.SetSuccessMsg($"Table {tableName} dropped sucessfully.");
        }
    }
}
