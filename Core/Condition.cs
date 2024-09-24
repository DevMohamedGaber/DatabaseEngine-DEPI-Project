namespace DatabaseEngine.Core
{
    public struct Condition
    {
        public string columnName { get; set; }
        public string operation { get; set; }
        public object value { get; set; }

        public Condition(string columnName, string operation, object value)
        {
            this.columnName = columnName;
            this.operation = operation;
            this.value = value;
        }
    }
}
