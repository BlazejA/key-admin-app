using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace ZadanieRekrutacyjne.Core
{
    public class TakenKeyCheckerPageViewModel : BaseViewModel
    {
        public ObservableCollection<TakenKeyViewModel> TakenKey { get; set; } = new ObservableCollection<TakenKeyViewModel>();

        public TakenKeyCheckerPageViewModel()
        {
            var list = DatabaseLocator.Database.EmployeeKeys
                .Include(x=>x.Employee)
                .Select(k=>new TakenKeyViewModel { 
                    KeyNumber= k.Key.KeyNumber,
                    EmployeeName = k.Employee.Name,
                    EmployeeLastName = k.Employee.LastName,
                    EmployeeNumber = k.Employee.EmployeeNumber
                })
                .ToList();

            foreach (var x in list)
            {
                TakenKey.Add(x);
            }
        }
    }
}
