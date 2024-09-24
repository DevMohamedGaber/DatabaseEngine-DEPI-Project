using DatabaseEngine.Core;
using DatabaseEngine.Queries.Types;
using DatabaseEngine.User;

namespace DatabaseEngine.Queries
{
    public abstract class Query
    {
        public List<string> statment { get; protected set; }
        public QueryResult Result { get; protected set; }
        public bool Success { get; protected set; }

        protected Table? _table;
        protected List<Condition> conditions;

        public Query(string[] statment)
        {
            this.statment = statment.ToList();
            this.Result = new QueryResult();
            this.conditions = new List<Condition>();
        }

        public void SetStatment(string stmt)
        {
            this.statment = stmt.Split(" ").ToList();
        }
        public static Query? Parse(string stmt, out string Error)
        {
            string[] stmtArr = stmt.Split(" ");

            if (stmtArr.Length == 0)
            {
                Error = "Can't process an empty statment";
                return null;
            }

            string pathKeyword = stmtArr[0].ToLower();
            Query? query = null;

            switch (pathKeyword)
            {
                case "create":
                    query = new CreateQuery(stmtArr);
                    break;
                case "drop":
                    query = new DropQuery(stmtArr);
                    break;
                case "select":
                    query = new SelectQuery(stmtArr);
                    break;
                case "insert":
                    query = new InsertQuery(stmtArr);
                    break;
                case "update":
                    query = new UpdateQuery(stmtArr);
                    break;
                case "delete":
                    query = new DeleteQuery(stmtArr);
                    break;
                default:
                    Error = "Unkown Command: the command you used isn't supported use one of the following \n[create, Drop, select, insert into, update, delete]";
                    break;
            }
            Error = string.Empty;
            return query;
        }

        public bool HasErrors()
        {
            return Result.Errors?.Count > 0;
        }

        public void TryExecute()
        {
            Execute();

            if (!HasErrors())
            {
                return;
            }

            foreach (string err in Result.Errors)
            {
                UserHandler.SetErrorMsg(err);
            }
        }

        protected bool ParseConditions()
        {
            int keywordIndex = QueryHelpers.GetKeywordIndex(statment, "where");

            if (keywordIndex == -1)
            {
                return false; // no condition in the query
            }

            if(statment.Count() < keywordIndex + 2)
            {
                Result.AddSyntaxError("a condition started but no parametars given after 'where'.");
                return false;
            }

            for (int i = keywordIndex + 1; i < statment.Count(); i++)
            {
                string conditionString = statment[i];
                string operation = QueryHelpers.GetOperationInCondition(conditionString);
                if(operation == string.Empty)
                {
                    Result.AddSyntaxError($"no operation given in '... {conditionString} ...'.");
                    return false;
                }

                string[] conditionArr = conditionString.Split(operation);

                if (!_table.CheckColumnExists(conditionArr[0]))
                {
                    Result.AddProcessError($"no column named '{conditionArr[0]}' found in '{_table.Name}' table.");
                    return false;
                }

                Condition condition = new Condition(conditionArr[0], operation, conditionArr[1]);
                conditions.Add(condition);
            }
            return true;
        }

        protected abstract void Execute();
    }
}
