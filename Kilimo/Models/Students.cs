namespace Kilimo.Models
{
    public class Student
    {
        public string StudentName { get; set; }
        public string StudentId { get; set; }
        public int ClassStreamId { get; set; }
    }
    public class StudentViewModel
    {
        public string StudentName { get; set; }
        public string StudentId { get; set; }
        public int ClassStreamId { get; set; }

        public IEnumerable<ClassStream> ClassStreams { get; set; }
    }
}
