namespace Fochso.Entities
{
    public class Class :BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
		//public int StudentId { get; set; }

		public ICollection<Student> Students { get; set; } = new HashSet<Student>();
    }
}
