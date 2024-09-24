using DatabaseEngine.Core;
using DatabaseEngine.FileManager;
using DatabaseEngine.User;

namespace DatabaseEngine.Queries.Types
{
    public class DeleteQuery : Query
    {
        public DeleteQuery(string[] stmt) : base(stmt) { }
        protected override void Execute()
        {
            if (statment.Count() < 2 || statment[1].ToLower() != "from")
            {
                Result.AddSyntaxError("query is missing 'from' statment");
                return;
            }

            if (statment.Count() < 3)
            {
                Result.AddSyntaxError("No table name was given");
                return;
            }

            _table = FileHandler.Read(statment[2]);

            if(_table == null)
            {
                Result.AddProcessError($"no table with name '{statment[2]}' found");
                return;
            }

            if(!ParseConditions())
            {
                return;
            }

            if(conditions.Count() == 0)
            {
                _table.Rows.Clear();
                return;
            }

            List<object[]> removed = new List<object[]>();

            foreach (string[] row in _table.Rows)
            {
                foreach (Condition condition in conditions)
                {
                    if(condition.Validate(row, ref _table))
                    {
                        removed.Add(row);
                    }
                }
            }

            if(removed.Count() > 0)
            {
                foreach (var item in removed)
                {
                    _table.Rows.Remove(item);
                }
            }

            _table.Save();

            UserHandler.SetSuccessMsg($"Removed {removed.Count()} records from table {_table.Name} successfully");
        }
    }
}
