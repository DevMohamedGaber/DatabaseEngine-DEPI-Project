using DatabaseEngine.Core;
using DatabaseEngine.Queries.Types;
using DatabaseEngine.User;

namespace DatabaseEngine.Queries
{
    public abstract class Query
    {
        public string[] statment { get; protected set; }
        public QueryResult Result { get; protected set; }
        public bool Success { get; protected set; }

        protected Table _table;

        public Query(string[] statment)
        {
            this.statment = statment;
            this.Result = new QueryResult();
        }

        public void SetStatment(string stmt)
        {
            this.statment = stmt.Split(" ");
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

        protected abstract void Execute();
    }
}
