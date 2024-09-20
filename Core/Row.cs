// set for future improvement
namespace DatabaseEngine.Core
{
    public struct Row
    {
        public object data { get; private set; }
        public bool isDirty { get; private set; }
    }
}
