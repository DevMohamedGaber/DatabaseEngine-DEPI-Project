namespace DatabaseEngine.Core
{
    public struct Column
    {
        public string Name;
        public ColumnType Type;

        public Column(string Name, ColumnType Type)
        {
            this.Name = Name;
            this.Type = Type;
        }
        public Column(string[] column)
        {
            this.Name = column[0];
            Enum.TryParse(column[1], out this.Type);
        }

        public override string ToString()
        {
            return $"{Name} {Type.ToString()}";
        }
    }
}
