using DatabaseEngine.Queries;
using DatabaseEngine.User;

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
                Query query = Query.Parse(stmt, out string errMsg);
                if(query == null)
                {
                    Console.WriteLine(errMsg);
                    continue;
                }
                query.Execute();
            }
        }
    }
}