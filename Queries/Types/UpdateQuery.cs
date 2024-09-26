using DatabaseEngine.Core;
using DatabaseEngine.FileManager;
using DatabaseEngine.User;

namespace DatabaseEngine.Queries.Types
{
    public class UpdateQuery : Query
    {
        public UpdateQuery(string[] stmt) : base(stmt) { }
        protected override void Execute()
        {
            if(statment.Count() < 1)
            {
                Result.AddSyntaxError("no table name was given");
                return;
            }

            _table = FileHandler.Read(statment[1]);

            if (_table == null)
            {
                Result.AddProcessError($"no table was found named '{statment[3]}'");
                return;
            }

            if(statment.Count() < 3)
            {
                Result.AddSyntaxError("missing 'set' keyword");
                return;
            }

            if (statment.Count() < 4)
            {
                Result.AddSyntaxError("no 'column=value' sets were given");
                return;
            }

            int stopLoopAt = QueryHelpers.GetKeywordIndex(statment, "where");
            List<UpdateSet> newvalues = new List<UpdateSet>();

            if(stopLoopAt == -1)
            {
                stopLoopAt = statment.Count();
            }

            for (int i = 3; i < stopLoopAt; i++)
            {
                string[] updateSet = statment[i].Replace(",", "").Split('=');
                int colIndex = _table.GetColumnIndexByName(updateSet[0]);
                if (colIndex == -1)
                {
                    Result.AddProcessError($"column '{updateSet[0]}' not found");
                    return;
                }
                newvalues.Add(new UpdateSet(colIndex, updateSet[1]));
            }

            if (!ParseConditions() && Result.Errors.Count() > 0)
            {
                return;
            }

            int counter = 0;

            for (int i = 0; i < _table.Rows.Count(); i++)
            {
                object[] row = _table.Rows[i];
                if(conditions.Count() > 0)
                {
                    bool meetsConditions = true;
                    foreach(Condition cond in conditions)
                    {
                        if(!cond.Validate(row, ref _table))
                        {
                            meetsConditions = false;
                            break;
                        }
                    }
                    if(!meetsConditions)
                    {
                        continue;
                    }
                }
                foreach(var updateValue in newvalues)
                {
                    row[updateValue.index] = updateValue.value;
                }
                _table.UpdateRow(i, row);
                counter++;
            }

            _table.Save();
            UserHandler.SetSuccessMsg($"Updated {counter} recoreds successfully");
        }

        private struct UpdateSet
        {
            public int index;
            public object value;
            
            public UpdateSet(int index, object value)
            {
                this.index = index;
                this.value = value;
            }
        }
    }
}
