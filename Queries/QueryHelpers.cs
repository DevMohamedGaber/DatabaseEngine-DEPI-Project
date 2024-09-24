namespace DatabaseEngine.Queries
{
    public static class QueryHelpers
    {
        private static readonly string[] keywords = ["create", "drop", "select", "insert", "into", "delete", "update", "where"];
        private static readonly string[] arithmeticOperators = ["=", "!=", ">", ">=", "<", "<="];
        private static readonly string[] logicalOperators = ["AND", "OR"];
        public static readonly string rowSeparator = "@@";
        public static Type GetTypeByName(string typeName)
        {
            Type type = null;
            switch(typeName)
            {
                case "int":
                    type = typeof(int);
                    break;
                case "float": 
                    type = typeof(float);
                    break;
                case "text":
                    type = typeof(string);
                    break;
                default:
                    type = typeof(object);
                    break;
            }
            return type;
        }

        public static bool IsKeyword(string word)
        {
            word = word.ToLower();

            foreach (string keyword in keywords)
            {
                if (keyword == word)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsArithmaticOperator(string word)
        {
            foreach (string op in arithmeticOperators)
            {
                if (op == word)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsLogicalOperator(string word)
        {
            word = word.ToLower();

            foreach (string op in logicalOperators)
            {
                if (op == word)
                {
                    return true;
                }
            }

            return false;
        }

        public static int GetKeywordIndex(List<string> statment, string keyword)
        {
            if(statment.Count() == 0)
            {
                return -1;
            }

            for (int i = 0; i < statment.Count(); i++)
            {
                if (statment[i].ToLower() == keyword)
                {
                    return 1;
                }
            }

            return -1;
        }

        public static string GetOperationInCondition(string condition)
        {
            foreach (var op in arithmeticOperators)
            {
                if(condition.Contains(op))
                {
                    return op;
                }
            }

            return string.Empty;
        }
        
    }
}
