using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ZadanieRekrutacyjne.Core
{
    public class TakeReturnKeyPageViewModel : BaseViewModel
    {
        public string TakenKeyId { get; set; }
        public string TakenEmployeeNumber { get; set; }
        public string TakenEmployeeName { get; set; }
        public string TakenEmployeeLastName { get; set; }
        public ICommand TakeNewKeyCommand { get; set; }
        public ICommand ReturnKeyCommand { get; set; }
        public bool HasErrorOccured { get; set; } = false;
        public bool HasManyEmployeesExist { get; set; } = false;
        public string ErrorMessage { get; set; }
        public string SucceedMessage { get; set; }
        public bool SucceedOccured { get; set; } = false;

        public TakeReturnKeyPageViewModel()
        {
            TakeNewKeyCommand = new RelayCommand(TakeNewKey);
            ReturnKeyCommand = new RelayCommand(ReturnKey);
        }

        private void TakeNewKey(object obj)
        {
            if (IsOnlyEmployee())
            {
                var takenEmployee = DatabaseLocator.Database.Employees.FirstOrDefault(x => x.Name == TakenEmployeeName && x.LastName == TakenEmployeeLastName && x.EmployeeNumber == TakenEmployeeNumber);
                if (!isNull(takenEmployee))
                {
                    if (!IsKeyTaken() && IsKeyExist(TakenKeyId))
                    {
                        var takenKey = DatabaseLocator.Database.Keys.FirstOrDefault(x => x.KeyNumber == TakenKeyId);
                        DatabaseLocator.Database.EmployeeKeys.Add(new EmployeeKey { KeyId = takenKey.Id, EmployeeId = takenEmployee.Id });
                        DatabaseLocator.Database.SaveChanges();
                        SucceedMessage = "Wydano klucz!";
                        SucceedOccured = true;
                        TakenKeyId = string.Empty;
                        TakenEmployeeNumber = string.Empty;
                        TakenEmployeeName = string.Empty;
                        TakenEmployeeLastName = string.Empty;

                    }
                }
            }
        }

        private void ReturnKey(object o)
        {
            if (IsDataCorrect(TakenKeyId))
            {
                var emplToRemove = DatabaseLocator.Database.EmployeeKeys.FirstOrDefault(x => x.Key.KeyNumber == TakenKeyId);
                if (emplToRemove != null)
                {
                    DatabaseLocator.Database.EmployeeKeys.Remove(emplToRemove);
                    DatabaseLocator.Database.SaveChanges();
                    SucceedMessage = "Zwrócono klucz!";
                    SucceedOccured = true;

                    TakenKeyId = string.Empty;
                    TakenEmployeeNumber = string.Empty;
                    TakenEmployeeLastName = string.Empty;
                    TakenEmployeeName = string.Empty;
                }
            }
        }

        private bool IsOnlyEmployee()
        {
            var found = DatabaseLocator.Database.Employees.Where(x => x.Name == TakenEmployeeName && x.LastName == TakenEmployeeLastName).ToList();
            if (found.Count == 1)
            {
                TakenEmployeeNumber = found[0].EmployeeNumber;
                return true;
            }
            else if (found.Count > 1 && !string.IsNullOrEmpty(TakenEmployeeNumber))
            {
                found.Where(x => x.EmployeeNumber == TakenEmployeeNumber);
                return true;
            }
            else if (IsAllFieldsFill())
            {
                return false;
            }
            else if (found.Count == 0)
            {
                HasErrorOccured = true;
                ErrorMessage = "Brak pracownika w bazie!";
                return false;
            }
            else
            {
                HasManyEmployeesExist = true;
                HasErrorOccured = true;
                ErrorMessage = "Wpisz numer pracownika!";
                return false;
            }
        }

        private bool IsAllFieldsFill()
        {
            if (string.IsNullOrEmpty(TakenKeyId) || string.IsNullOrEmpty(TakenEmployeeName) || string.IsNullOrEmpty(TakenEmployeeLastName))
            {
                HasErrorOccured = true;
                ErrorMessage = "Wpisz wszystkie dane!";
                return false;
            }
            return true;
        }

        private bool isNull(object found)
        {
            if (string.IsNullOrEmpty(TakenEmployeeName) || string.IsNullOrEmpty(TakenEmployeeLastName) || found == null)
            {
                HasErrorOccured = true;
                ErrorMessage = "Brak pracownika w bazie!";
                return true;
            }
            else if (string.IsNullOrEmpty(TakenKeyId))
            {
                HasErrorOccured = true;
                ErrorMessage = "Wpisz numer klucza!";
                return true;
            }
            else
            {
                HasErrorOccured = false;
                ErrorMessage = string.Empty;
                return false;
            }
        }
        private bool IsDataCorrect(string key)
        {
            var takenAllKeys = DatabaseLocator.Database.EmployeeKeys
                .Where(x => x.Key.KeyNumber == key)
                .Select(k => new TakenKeyViewModel
                {
                    KeyNumber = k.Key.KeyNumber,
                    EmployeeName = k.Employee.Name,
                    EmployeeLastName = k.Employee.LastName,
                    EmployeeNumber = k.Employee.EmployeeNumber
                })
                .ToList();
            if (takenAllKeys.Count > 0)
            {
                foreach (var x in takenAllKeys)
                {
                    if (x.EmployeeName == TakenEmployeeName && x.EmployeeLastName == TakenEmployeeLastName && x.KeyNumber == TakenKeyId)
                    {
                        return true;
                    }
                    else
                    {
                        HasErrorOccured = true;
                        ErrorMessage = "Niepoprawny numer klucza lub dane pracownika!";
                        return false;
                    }
                }
            }
            else
            {
                HasErrorOccured = true;
                ErrorMessage = "Brak klucza w bazie!";
                return false;
            }
            return false;
        }
        private bool IsKeyExist(string keyNumber)
        {
            var takenKey = DatabaseLocator.Database.Keys.Any(x => x.KeyNumber == keyNumber);
            if (takenKey == false)
            {
                HasErrorOccured = true;
                ErrorMessage = "Brak klucza w bazie!";
                return false;
            }
            else
            {
                HasErrorOccured = false;
                return true;
            }
        }
        private bool IsKeyTaken()
        {
            var takenAllKeys = DatabaseLocator.Database.EmployeeKeys.Any(x => x.Key.KeyNumber == TakenKeyId);
            if (takenAllKeys)
            {
                HasErrorOccured = true;
                ErrorMessage = "Klucz został pobrany!";
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
