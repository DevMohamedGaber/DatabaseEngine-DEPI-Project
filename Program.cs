namespace DatabaseEngine
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            // program loop
            while (true)
            {
                // get user input
                string stmt = UserHandler.GetUserStatment();
                // handle user input
                Query query = new Query(stmt);
                query.Resolve();
            }
        }
    }
}