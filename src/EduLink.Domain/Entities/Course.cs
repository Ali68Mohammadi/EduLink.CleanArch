namespace EduLink.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
         
        //navigation Property
        public int AcademyId { get; set; }
    }
}