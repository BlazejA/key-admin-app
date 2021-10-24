using System.Collections.Generic;

namespace ZadanieRekrutacyjne
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeNumber { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public IList<EmployeeKey> EmployeeKeys { get; set; } = new List<EmployeeKey>();
    }
}
