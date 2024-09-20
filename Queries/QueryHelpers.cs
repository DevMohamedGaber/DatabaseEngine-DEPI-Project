namespace DatabaseEngine.Queries
{
    public static class QueryHelpers
    {
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
    }
}
