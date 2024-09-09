namespace DatabaseEngine
{
    public struct QueryResult
    {
        public List<object> values { get; set; }
        public List<string> Errors { get; set; }
    }
}
