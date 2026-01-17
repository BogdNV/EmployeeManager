namespace EmployeeManager.Domain.Rules
{
    public static class EmployeeValidationRules
    {
        public static void ValidationFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("Имя обязательно");

        }

        public static void ValidationSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
                throw new DomainException("Фамилия обязательна");

        }

        public static void ValidationDateOfBirth(DateTime date)
        {
            var yearDiff = DateTime.Now.Year - date.Year;
            if (yearDiff > 100 || yearDiff < 0)
                throw new DomainException("Некорректный возраст");
        }
    }

    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {

        }
    }
}