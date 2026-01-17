using EmployeeManager.Domain.Rules;

namespace EmployeeManager.Domain.Entities
{
    public class Employee
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        public string Patronymic { get; private set; }
        public string Address { get; private set; }
        public string Department { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string AboutMe { get; private set; }

        private Employee() { }

        public static Employee Create(
            int id,
            string firstName,
            string surname,
            string patronymic,
            string address,
            string department,
            DateTime dateOfBirth,
            string aboutMe
        )
        {
            var employee = new Employee
            {
                Id = id,
                FirstName = firstName,
                Surname = surname,
                Patronymic = patronymic,
                Address = address,
                Department = department,
                DateOfBirth = dateOfBirth,
                AboutMe = aboutMe
            };

            employee.Validate();

            return employee;
        }

        private void Validate()
        {
            EmployeeValidationRules.ValidationFirstName(FirstName);
            EmployeeValidationRules.ValidationSurname(Surname);
            EmployeeValidationRules.ValidationDateOfBirth(DateOfBirth);
        }
    }
}