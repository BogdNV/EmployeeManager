namespace EmployeeManager.Application.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AboutMe { get; set; }
    }
}