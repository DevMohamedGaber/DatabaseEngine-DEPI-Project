using DatabaseEngine.Queries;

namespace DatabaseEngine.Core
{
    public struct Column
    {
        public string Name;
        public Type Type;

        public Column(string Name, Type Type)
        {
            this.Name = Name;
            this.Type = Type;
        }
        public Column(string[] column)
        {
            this.Name = column[0];
            this.Type = QueryHelpers.GetTypeByName(column[1]);
        }

        public override string ToString()
        {
            return $"{Name} {Type.Name}";
        }
    }
}
