using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ZadanieRekrutacyjne.Database;

namespace ZadanieRekrutacyjne.Core
{
    public class KeysListPageViewModel : BaseViewModel
    {
        public ObservableCollection<KeysViewModel> KeysList { get; set; } = new ObservableCollection<KeysViewModel>();

        public string NewKeyID { get; set; }
        public string NewRoomName { get; set; }
        public ICommand AddNewKeyCommand { get; set; }
        public ICommand DeleteSelectedKeyCommand { get; set; }
        public ICommand UpdateKeyCommand { get; set; }
        public ICommand ToogleVisibilityCommand { get; set; }
        public bool HasErrorOccured { get; set; } = false;
        public bool DisplayControl { get; set; } = false;
        public string UpdatedKeyID { get; set; }
        public string UpdatedRoomName { get; set; }

        public KeysListPageViewModel()
        {
            AddNewKeyCommand = new RelayCommand(AddNewKey);
            DeleteSelectedKeyCommand = new RelayCommand(DeleteSelectedKey);
            ToogleVisibilityCommand = new RelayCommand(ToogleVisibility);
            UpdateKeyCommand = new RelayCommand(UpdateKey);
            LoadKeys();
            
        }
        private void LoadKeys()
        {
            KeysList.Clear();
            foreach (var key in DatabaseLocator.Database.Keys.ToList())
            {                
                KeysList.Add(new KeysViewModel { KeyNumber = key.KeyNumber, RoomName = key.RoomName });
            }
        }

        private void AddNewKey(object o)
        {
            var newKey = new KeysViewModel { KeyNumber = NewKeyID, RoomName = NewRoomName };

            if (DatabaseLocator.Database.Keys.Any(x => x.KeyNumber == newKey.KeyNumber))
            {
                HasErrorOccured = true;
                OnPropertyChanged(nameof(HasErrorOccured));
            }
            else
            {
                DatabaseLocator.Database.Keys.Add(new Key { KeyNumber = newKey.KeyNumber, RoomName = newKey.RoomName });
                KeysList.Add(newKey);
                DatabaseLocator.Database.SaveChanges();

                HasErrorOccured = false;
                OnPropertyChanged(nameof(HasErrorOccured));
            }

            NewKeyID = string.Empty;
            NewRoomName = string.Empty;

            OnPropertyChanged(nameof(NewKeyID));
            OnPropertyChanged(nameof(NewRoomName));
        }
        private void DeleteSelectedKey(object o)
        {
            var selectedKeys = KeysList.Where(x => x.IsSelected).ToList();

            foreach (var key in selectedKeys)
            {
                KeysList.Remove(key);
                var keyToRemove = DatabaseLocator.Database.Keys.FirstOrDefault(x => x.KeyNumber == key.KeyNumber);
                if (keyToRemove != null)
                {
                    DatabaseLocator.Database.Keys.Remove(keyToRemove);
                }
            }

            DatabaseLocator.Database.SaveChanges();
        }
        private void ToogleVisibility(object o)
        {
            DisplayControl = !DisplayControl;
            OnPropertyChanged(nameof(DisplayControl));
        }
        private void UpdateKey(string id)
        {
            var selectedKeys = KeysList.Where(x => x.IsSelected).ToList();
            foreach (var key in selectedKeys)
            {
                var keyToRemove = DatabaseLocator.Database.Keys.FirstOrDefault(x => x.KeyNumber == key.KeyNumber);
                if (keyToRemove != null)
                {
                    if(!string.IsNullOrEmpty(UpdatedKeyID))
                        keyToRemove.KeyNumber = UpdatedKeyID;
                    else if(!string.IsNullOrEmpty(UpdatedRoomName))
                        keyToRemove.RoomName = UpdatedRoomName;
                    else if(!string.IsNullOrEmpty(UpdatedKeyID) && !string.IsNullOrEmpty(UpdatedRoomName))
                    {
                        keyToRemove.RoomName = UpdatedRoomName;
                        keyToRemove.KeyNumber = UpdatedKeyID;
                    }
                    LoadKeys();
                }
            }
            DatabaseLocator.Database.SaveChanges();
        }
    }
}
