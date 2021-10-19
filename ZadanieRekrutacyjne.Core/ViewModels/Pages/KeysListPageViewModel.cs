using System;
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

        public KeysListPageViewModel()
        {
            AddNewKeyCommand = new RelayCommand(AddNewKey);
            DeleteSelectedKeyCommand = new RelayCommand(DeleteSelectedKey);

            foreach (var key in DatabaseLocator.Database.Keys.ToList())
            {
                KeysList.Add(new KeysViewModel { KeyID = key.KeyID, RoomName = key.RoomName });
            }
        }

        private void AddNewKey()
        {
            var newKey = new KeysViewModel { KeyID = NewKeyID, RoomName = NewRoomName };
            KeysList.Add(newKey);
            DatabaseLocator.Database.Keys.Add(new Key { KeyID = newKey.KeyID, RoomName = newKey.RoomName });
            DatabaseLocator.Database.SaveChanges();
            
            NewKeyID = string.Empty;
            NewRoomName = string.Empty;

            OnPropertyChanged(nameof(NewKeyID));
            OnPropertyChanged(nameof(NewRoomName));
        }

        private void DeleteSelectedKey()
        {
            var selectedKeys = KeysList.Where(x => x.IsSelected).ToList();

            foreach (var key in selectedKeys)
            {
                KeysList.Remove(key);
                var keyToRemove = DatabaseLocator.Database.Keys.FirstOrDefault(x => x.KeyID == key.KeyID);
                if (keyToRemove != null)
                {
                    DatabaseLocator.Database.Keys.Remove(keyToRemove);
                }
            }

            DatabaseLocator.Database.SaveChanges();
        }
    }
}
