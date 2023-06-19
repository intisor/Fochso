namespace Fochso.Entities
{
    public class Student : BaseEntity
    {
        public string Name { get; set; }
        public string Class { get; set;}
        public int ClassId { get; set;}
        public Class ClassClass { get; set;}
    }
}
