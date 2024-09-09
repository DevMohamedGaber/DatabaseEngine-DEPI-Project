namespace DatabaseEngine.Core
{
    public class Table
    {
        public string Name { get; private set; }
        public List<Column> Columns { get; private set; }
        public List<Row> Rows { get; private set; }


    }
}
