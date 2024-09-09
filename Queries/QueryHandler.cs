namespace DatabaseEngine.Queries
{
    public static class QueryHandler
    {
        public static QueryResult Parse(Query query)
        {
            QueryResult result = default;
            string pathKeyword = query.statment[0].ToLower();

            switch (pathKeyword)
            {
                case "create":
                    result = HandleCreate(query);
                    break;
                case "drop":
                    result = HandleDrop(query);
                    break;
                case "select":
                    result = HandleSelect(query);
                    break;
                case "insert":
                    result = HandleInsert(query);
                    break;
                case "update":
                    result = HandleUpdate(query);
                    break;
                case "delete":
                    result = HandleDelete(query);
                    break;
            }

            return result;
        }

        private static QueryResult HandleCreate(Query query)
        {
            QueryResult result = default;

            return result;
        }
        private static QueryResult HandleDrop(Query query)
        {
            QueryResult result = default;

            return result;
        }
        private static QueryResult HandleSelect(Query query)
        {
            QueryResult result = default;

            return result;
        }
        private static QueryResult HandleInsert(Query query)
        {
            QueryResult result = default;

            return result;
        }
        private static QueryResult HandleUpdate(Query query)
        {
            QueryResult result = default;

            return result;
        }
        private static QueryResult HandleDelete(Query query)
        {
            QueryResult result = default;

            return result;
        }
    }
}
