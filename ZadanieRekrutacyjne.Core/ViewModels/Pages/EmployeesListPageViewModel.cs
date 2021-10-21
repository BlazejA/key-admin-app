using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace ZadanieRekrutacyjne.Core
{
    public class EmployeesListPageViewModel : BaseViewModel
    {
        public ObservableCollection<EmployeesViewModel> EmployeesList { get; set; } = new ObservableCollection<EmployeesViewModel>();
        public string NewEmployeeNumber { get; set; }
        public string NewName { get; set; }
        public string NewLastName { get; set; }
        public string NewPosition { get; set; }
        public string NewDepartment { get; set; }
        public ICommand AddNewEmployeeCommand { get; set; }
        public ICommand DeleteSelectedEmployeeCommand { get; set; }
        public ICommand UpdateEmployeeCommand { get; set; }
        public ICommand ToogleEditVisibilityCommand { get; set; }
        public ICommand ToogleAddVisibilityCommand { get; set; }
        public bool HasErrorOccured { get; set; } = false;
        public bool EditDisplayControl { get; set; } = false;
        public bool AddDisplayControl { get; set; } = false;
        public string UpdatedEmployeeNumber { get; set; }
        public string UpdatedName { get; set; }
        public string UpdatedLastName { get; set; }
        public string UpdatedPosition { get; set; }
        public string UpdatedDepartment { get; set; }

        public EmployeesListPageViewModel()
        {
            AddNewEmployeeCommand = new RelayCommand(AddNewEmployee);
            DeleteSelectedEmployeeCommand = new RelayCommand(DeleteSelectedEmployee);
            ToogleEditVisibilityCommand = new RelayCommand(ToogleEditVisibility);
            ToogleAddVisibilityCommand = new RelayCommand(ToogleAddVisibility);
            UpdateEmployeeCommand = new RelayCommand(UpdateEmployee);

            foreach (var Empl in DatabaseLocator.Database.Employees.ToList())
            {
                EmployeesList.Add(new EmployeesViewModel
                {
                    EmployeeNumber = Empl.EmployeeNumber,
                    Name = Empl.Name,
                    LastName = Empl.LastName,
                    Position = Empl.Position,
                    Department = Empl.Department
                });
            }
        }
        private void AddNewEmployee(object o)
        {
            var newEmployee = new EmployeesViewModel
            {
                EmployeeNumber = NewEmployeeNumber,
                Name = NewName,
                LastName = NewLastName,
                Position = NewPosition,
                Department = NewDepartment
            };

            if (!EmployeeNumberExist(newEmployee.EmployeeNumber))
            {
                DatabaseLocator.Database.Employees.Add(new Employee
                {
                    EmployeeNumber = newEmployee.EmployeeNumber,
                    Name = newEmployee.Name,
                    LastName = newEmployee.LastName,
                    Position = newEmployee.Position,
                    Department = newEmployee.Department
                });
                EmployeesList.Add(newEmployee);
                DatabaseLocator.Database.SaveChanges();
            }

            NewEmployeeNumber = string.Empty;
            NewName = string.Empty;
            NewLastName = string.Empty;
            NewName = string.Empty;
            NewDepartment = string.Empty;

            OnPropertyChanged(nameof(NewEmployeeNumber));
            OnPropertyChanged(nameof(NewName));
            OnPropertyChanged(nameof(NewLastName));
            OnPropertyChanged(nameof(NewName));
            OnPropertyChanged(nameof(NewName));

        }
        private void DeleteSelectedEmployee(object o)
        {
            var selectedEmployees = EmployeesList.Where(x => x.IsSelected).ToList();

            foreach (var Empl in selectedEmployees)
            {
                EmployeesList.Remove(Empl);
                var emplToRemove = DatabaseLocator.Database.Employees.FirstOrDefault(x => x.EmployeeNumber == Empl.EmployeeNumber);
                if (emplToRemove != null)
                {
                    DatabaseLocator.Database.Employees.Remove(emplToRemove);
                }
            }

            DatabaseLocator.Database.SaveChanges();
        }
        private void ToogleEditVisibility(object o)
        {
            EditDisplayControl = !EditDisplayControl;
            OnPropertyChanged(nameof(EditDisplayControl));
        }
        private void ToogleAddVisibility(object o)
        {
            AddDisplayControl = !AddDisplayControl;
            OnPropertyChanged(nameof(AddDisplayControl));
        }
        private void UpdateEmployee(string id)
        {
            Debug.WriteLine("BLAD");
            var selectedEmployees = EmployeesList.Where(x => x.IsSelected).ToList();
            foreach (var empl in selectedEmployees)
            {
                var employeeToUpdate = DatabaseLocator.Database.Employees.FirstOrDefault(x => x.EmployeeNumber == empl.EmployeeNumber);
                if (employeeToUpdate != null && !EmployeeNumberExist(UpdatedEmployeeNumber))
                {
                    if (string.IsNullOrEmpty(UpdatedEmployeeNumber))
                    {
                        UpdatedEmployeeNumber = empl.EmployeeNumber;
                    }

                    if (string.IsNullOrEmpty(UpdatedName))
                    {
                        UpdatedName = empl.Name;
                    }

                    if (string.IsNullOrEmpty(UpdatedLastName))
                    {
                        UpdatedLastName = empl.LastName;
                    }

                    if (string.IsNullOrEmpty(UpdatedPosition))
                    {
                        UpdatedPosition = empl.Position;
                    }

                    if (string.IsNullOrEmpty(UpdatedDepartment))
                    {
                        UpdatedDepartment = empl.Department;
                    }

                    var newItem = new EmployeesViewModel
                    {
                        EmployeeNumber = UpdatedEmployeeNumber,
                        Name = UpdatedName,
                        LastName = UpdatedLastName,
                        Position = UpdatedPosition,
                        Department = UpdatedDepartment
                    };
                    employeeToUpdate.EmployeeNumber = UpdatedEmployeeNumber;
                    employeeToUpdate.Name = UpdatedName;
                    employeeToUpdate.LastName = UpdatedLastName;
                    employeeToUpdate.Position = UpdatedPosition;
                    employeeToUpdate.Department = UpdatedDepartment;

                    ReplaceItem(EmployeesList, empl.EmployeeNumber, newItem);

                }
            }
            UpdatedEmployeeNumber = string.Empty;
            UpdatedName = string.Empty;
            UpdatedLastName = string.Empty;
            UpdatedPosition = string.Empty;
            UpdatedDepartment = string.Empty;

            DatabaseLocator.Database.SaveChanges();
        }
        private bool EmployeeNumberExist(string keyToCheck)
        {
            if (DatabaseLocator.Database.Employees.Any(x => x.EmployeeNumber == keyToCheck))
            {
                HasErrorOccured = true;
                OnPropertyChanged(nameof(HasErrorOccured));
                return true;
            }
            else
            {
                HasErrorOccured = false;
                OnPropertyChanged(nameof(HasErrorOccured));
            }
            return false;
        }
        private void ReplaceItem(ObservableCollection<EmployeesViewModel> list, string toChange, EmployeesViewModel newItem)
        {
            var oldItem = list.FirstOrDefault(x => x.EmployeeNumber == toChange);
            var oldIndex = list.IndexOf(oldItem);
            list[oldIndex] = newItem;
        }
    }

}
