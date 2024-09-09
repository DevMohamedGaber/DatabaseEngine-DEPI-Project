namespace DatabaseEngine.User
{
    public static class UserHandler
    {
        public static string GetUserStatment()
        {
            string statment = GetUserInput("Please enter your query: ");

            while (statment == null || statment == string.Empty)
            {
                statment = GetUserInput("Please enter a valid query: ");
            }

            return statment;
        }
        private static string GetUserInput(string msg = null)
        {
            if (msg != null)
            {
                Console.WriteLine(msg);
            }

            return Console.ReadLine();
        }
    }
}
