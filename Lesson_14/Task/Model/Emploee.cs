
namespace Task
{
    public class Emploee
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }

        public Emploee(string firstName, string secondName, string middleName)
        {
            FirstName = firstName;
            SecondName = secondName;
            MiddleName = middleName;
        }
        public override string ToString()
        {
            return $"{SecondName} {FirstName} {MiddleName}";
        }
    }
}
