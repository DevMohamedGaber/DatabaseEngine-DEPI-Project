namespace DatabaseEngine.User
{
    public static class UserHandler
    {
        public static string GetUserStatment()
        {
            Console.ForegroundColor = ConsoleColor.White;
            string statment = GetUserInput("Please enter your query: ");

            while (statment == null || statment == string.Empty)
            {
                statment = GetUserInput("Please enter a valid query: ");
            }

            return statment;
        }
        public static void SetErrorMsg(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
        }
        public static void SetSuccessMsg(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
        }

        private static string GetUserInput(string msg = null)
        {
            if (msg != null)
            {
                Console.WriteLine("\n" + msg);
            }

            return Console.ReadLine();
        }

    }
}
