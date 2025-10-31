namespace MottuChallenge.Application.DTOs.Request;

public class EmployeeDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Guid YardId { get; set; }
    public string Password { get; set; }
}