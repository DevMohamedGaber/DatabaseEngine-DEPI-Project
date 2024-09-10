using DatabaseEngine.Queries.Types;

namespace DatabaseEngine.Queries
{
    public abstract class Query
    {
        public string[] statment { get; protected set; }
        public QueryResult Result { get; protected set; }
        public bool Success { get; protected set; }

        public Query(string[] statment)
        {
            this.statment = statment;
            this.Result = new QueryResult();
        }

        public void SetStatment(string stmt)
        {
            this.statment = stmt.Split(" ");
        }

        public void Resolve()
        {
            

            //Parse();

            if (Result.Errors.Count() > 0)
            {
                foreach (string err in Result.Errors)
                {
                    Console.WriteLine("\n" + err);
                }
            }
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
            Query query = null;

            switch (pathKeyword)
            {
                case "create":
                    query = new CreateQuery(stmtArr);
                    break;
                //case "drop":
                //    query = (DropQuery)this;
                //    break;
                //case "select":
                //    query = (SelectQuery)this;
                //    break;
                //case "insert":
                //    query = (InsertQuery)this;
                //    break;
                //case "update":
                //    query = (UpdateQuery)this;
                //    break;
                //case "delete":
                //    query = (DeleteQuery)this;
                //    break;
                default:
                    Error = "Unkown Command: the command you used isn't supported use one of the following \n[create, Drop, select, insert into, update, delete]";
                    break;
            }
            Error = string.Empty;
            return query;
        }

        public abstract void Execute();
    }
}
