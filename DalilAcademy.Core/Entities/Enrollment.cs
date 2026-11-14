using DalilAcademy.Core.Enums;

namespace DalilAcademy.Core.Entities;

public class Enrollment
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

    public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Pending;

    public Student Student { get; set; } = null!;

    public Course Course { get; set; } = null!;
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}