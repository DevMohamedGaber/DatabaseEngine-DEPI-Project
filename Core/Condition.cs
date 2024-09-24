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

        public bool Validate(object[] row, ref Table table)
        {
            int colIndex = table.GetColumnIndexByName(columnName);

            if (colIndex == -1)
            {
                return false;
            }

            Column column = table.Columns[colIndex];
            dynamic columnValue = Convert.ChangeType(row[colIndex], column.Type);
            dynamic conditionValue = Convert.ChangeType(value, column.Type);
            bool result = false;

            switch(operation)
            {
                case "=":
                    result = columnValue == conditionValue;
                    break;
                case "!=":
                    result = columnValue != conditionValue;
                    break;
                case ">":
                    result = columnValue > conditionValue;
                    break;
                case ">=":
                    result = columnValue >= conditionValue;
                    break;
                case "<":
                    result = columnValue < conditionValue;
                    break;
                case "<=":
                    result = columnValue <= conditionValue;
                    break;
            }

            return result;
        }
    }
}
