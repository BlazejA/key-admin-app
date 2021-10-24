using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ZadanieRekrutacyjne.Core
{
    public class TakeReturnKeyPageViewModel : BaseViewModel
    {
        public ObservableCollection<TakenKeyViewModel> TakenKeys { get; set; } = new ObservableCollection<TakenKeyViewModel>();
        public string TakenKeyId { get; set; }
        public string TakenEmployeeNumber { get; set; }
        public string TakenEmployeeName { get; set; }
        public string TakenEmployeeLastName { get; set; }
        public ICommand TakeNewKeyCommand { get; set; }
        public ICommand ReturnKeyCommand { get; set; }
        public bool HasErrorOccured { get; set; } = false;
        public bool HasManyEmployeesExist { get; set; } = false;
        public string ErrorMessage { get; set; }

        public TakeReturnKeyPageViewModel()
        {
            TakeNewKeyCommand = new RelayCommand(TakeNewKey);
            ReturnKeyCommand = new RelayCommand(ReturnKey);
        }

        private void TakeNewKey(object obj)
        {
            var takenKey = DatabaseLocator.Database.Keys.FirstOrDefault(x => x.KeyNumber == TakenKeyId);
            if (IsOnlyEmployee())
            {
                var takenEmployee = DatabaseLocator.Database.Employees.FirstOrDefault(x => x.Name == TakenEmployeeName && x.LastName == TakenEmployeeLastName && x.EmployeeNumber == TakenEmployeeNumber);
                if (!isNull(takenEmployee))
                {
                    if (IsEmployeeExist(takenEmployee.EmployeeNumber, TakenKeyId))
                    {
                        DatabaseLocator.Database.EmployeeKeys.Add(new EmployeeKey { KeyId = takenKey.Id, EmployeeId = takenEmployee.Id });
                        DatabaseLocator.Database.SaveChanges();
                        TakenKeyId = string.Empty;
                        TakenEmployeeNumber = string.Empty;
                        TakenEmployeeName = string.Empty;
                        TakenEmployeeLastName = string.Empty;

                    }
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
            else if (found.Count>1 && !string.IsNullOrEmpty(TakenEmployeeNumber))
            {
                found.Where(x => x.EmployeeNumber == TakenEmployeeNumber);
                return true;
            }
            else
            {
                HasManyEmployeesExist = true;
                ErrorMessage = "Wpisz numer pracownika!";
                return false;
            }
        }
        private void ReturnKey(object o)
        {
            var takenAllKeys = DatabaseLocator.Database.EmployeeKeys.ToList();
            if (IsNumbersCorrect(TakenKeyId))
            {
                var emplToRemove = DatabaseLocator.Database.EmployeeKeys.FirstOrDefault(x => x.Employee.EmployeeNumber == TakenEmployeeNumber);
                if (emplToRemove != null)
                {
                    var remove = TakenKeys.Where(x => x.EmployeeNumber == TakenEmployeeNumber).FirstOrDefault();
                    TakenKeys.Remove(remove);
                    DatabaseLocator.Database.EmployeeKeys.Remove(emplToRemove);
                }
            }
            TakenKeyId = string.Empty;
            TakenEmployeeNumber = string.Empty;
            TakenEmployeeLastName = string.Empty;
            TakenEmployeeName = string.Empty;
        }
        private bool IsEmployeeExist(string emplNumber, string keyNumber)
        {
            var takenEmployee = DatabaseLocator.Database.Employees.Any(x => x.EmployeeNumber == emplNumber);
            var takenKey = DatabaseLocator.Database.Keys.Any(x => x.KeyNumber == keyNumber);

            if (takenEmployee == false || takenKey == false)
            {
                HasErrorOccured = true;
                return false;
            }
            else
            {
                HasErrorOccured = false;
                return true;
            }
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
        private bool IsNumbersCorrect(string key)
        {
            var takenAllKeys = DatabaseLocator.Database.EmployeeKeys.Where(x => x.Key.KeyNumber == key).ToList();
            if (takenAllKeys != null)
            {
                foreach (var x in takenAllKeys)
                {
                    if (x.Employee.Name == TakenEmployeeName && x.Employee.LastName == TakenEmployeeLastName && x.Key.KeyNumber == TakenKeyId)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
    }
}
