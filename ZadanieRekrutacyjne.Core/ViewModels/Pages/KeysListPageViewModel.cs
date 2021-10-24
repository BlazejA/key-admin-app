using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ZadanieRekrutacyjne.Database;

namespace ZadanieRekrutacyjne.Core
{
    public class KeysListPageViewModel : BaseViewModel
    {
        public ObservableCollection<KeysViewModel> KeysList { get; set; } = new ObservableCollection<KeysViewModel>();
        public string NewKeyNumber { get; set; }
        public string NewRoomName { get; set; }
        public ICommand AddNewKeyCommand { get; set; }
        public ICommand DeleteSelectedKeyCommand { get; set; }
        public ICommand UpdateKeyCommand { get; set; }
        public ICommand ToogleEditVisibilityCommand { get; set; }
        public ICommand ToogleAddVisibilityCommand { get; set; }
        public bool HasErrorOccured { get; set; } = false;
        public bool EditDisplayControl { get; set; } = false;
        public bool AddDisplayControl { get; set; } = false;
        public string UpdatedKeyNumber { get; set; }
        public string UpdatedRoomName { get; set; }

        public KeysListPageViewModel()
        {
            AddNewKeyCommand = new RelayCommand(AddNewKey);
            DeleteSelectedKeyCommand = new RelayCommand(DeleteSelectedKey);
            ToogleEditVisibilityCommand = new RelayCommand(ToogleEditVisibility);
            ToogleAddVisibilityCommand = new RelayCommand(ToogleAddVisibility);
            UpdateKeyCommand = new RelayCommand(UpdateKey);

            foreach (var key in DatabaseLocator.Database.Keys.ToList())
            {
                KeysList.Add(new KeysViewModel { KeyNumber = key.KeyNumber, RoomName = key.RoomName });
            }
        }
        private void AddNewKey(object o)
        {
            var newKey = new KeysViewModel { KeyNumber = NewKeyNumber, RoomName = NewRoomName };

            if (!KeyNumberExist(newKey.KeyNumber))
            {
                DatabaseLocator.Database.Keys.Add(new Key { KeyNumber = newKey.KeyNumber, RoomName = newKey.RoomName });
                KeysList.Add(newKey);
                DatabaseLocator.Database.SaveChanges();
            }

            NewKeyNumber = string.Empty;
            NewRoomName = string.Empty;
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
        private void ToogleEditVisibility(object o)
        {
            EditDisplayControl = !EditDisplayControl;
        }
        private void ToogleAddVisibility(object o)
        {
            AddDisplayControl = !AddDisplayControl;
        }
        private void UpdateKey(string id)
        {
            var selectedKeys = KeysList.Where(x => x.IsSelected).ToList();
            foreach (var key in selectedKeys)
            {
                var keyToUpdate = DatabaseLocator.Database.Keys.FirstOrDefault(x => x.KeyNumber == key.KeyNumber);
                if (keyToUpdate != null && !KeyNumberExist(UpdatedKeyNumber))
                {
                    if (!string.IsNullOrEmpty(UpdatedKeyNumber))
                    {
                        keyToUpdate.KeyNumber = UpdatedKeyNumber;
                        var newItem = new KeysViewModel { KeyNumber = UpdatedKeyNumber, RoomName = key.RoomName };
                        ReplaceItem(KeysList, key.KeyNumber, newItem);
                    }
                    else if (!string.IsNullOrEmpty(UpdatedRoomName))
                    {
                        keyToUpdate.RoomName = UpdatedRoomName;
                        var newItem = new KeysViewModel { KeyNumber = key.KeyNumber, RoomName = UpdatedRoomName };
                        ReplaceItem(KeysList, key.KeyNumber, newItem);
                    }
                    else if (!string.IsNullOrEmpty(UpdatedKeyNumber) && !string.IsNullOrEmpty(UpdatedRoomName))
                    {
                        keyToUpdate.KeyNumber = UpdatedKeyNumber;

                        keyToUpdate.RoomName = UpdatedRoomName;
                        var newItem = new KeysViewModel { KeyNumber = UpdatedKeyNumber, RoomName = UpdatedRoomName };
                        ReplaceItem(KeysList, key.KeyNumber, newItem);
                    }
                }
            }

            UpdatedKeyNumber = string.Empty;
            UpdatedRoomName = string.Empty;

            DatabaseLocator.Database.SaveChanges();
        }
        private bool KeyNumberExist(string keyToCheck)
        {
            if (DatabaseLocator.Database.Keys.Any(x => x.KeyNumber == keyToCheck))
            {
                HasErrorOccured = true;
                return true;
            }
            else
            {
                HasErrorOccured = false;
                return false;
            }
        }
        private void ReplaceItem(ObservableCollection<KeysViewModel> list, string toChange, KeysViewModel newItem)
        {
            var oldItem = list.FirstOrDefault(x => x.KeyNumber == toChange);
            var oldIndex = list.IndexOf(oldItem);
            list[oldIndex] = newItem;
        }
    }
}
