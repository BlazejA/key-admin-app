using System.Collections.Generic;

namespace ZadanieRekrutacyjne
{
    public class Key
    {
        public int Id { get; set; }
        public string KeyNumber { get; set; }
        public string RoomName { get; set; }

        public IList<EmployeeKey> EmployeeKeys { get; set; } = new List<EmployeeKey>();

    }
}
