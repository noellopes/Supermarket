using Microsoft.EntityFrameworkCore;

namespace Supermarket.Models {

    [PrimaryKey(nameof(StudentId), nameof(TaskId))]
    public class StudentTask {
        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public int TaskId { get; set; }
        public Task? Task { get; set; }

        public string? Observations { get; set; }

        public ICollection<Student>? Students { get; set; }
        public ICollection<Task>? Tasks { get; set; }
    }
}