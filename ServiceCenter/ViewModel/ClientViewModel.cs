using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.ViewModel
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using ServiceCenter.Models;
    using ServiceCenter.View;
    using ServiceCenter.Extensions;

    public class ClientViewModel : INotifyPropertyChanged
    {
        ServiceCenterContext db;
        RelayCommand addCommand;
        RelayCommand deleteCommand;
        RelayCommand editCommand;
        Client selectedClient;
        ObservableCollection<Client> clients;

        public ObservableCollection<Client> Clients
        {
            get => clients;
            set
            {
                clients = value;
                OnPropertyChanged("Clients");
            }
        }

        public Client SelectedClient
        {
            get => selectedClient;
            set
            {
                selectedClient = value;
                OnPropertyChanged("SelectedClient");
            }
        }

        public ClientViewModel()
        {
            db = new ServiceCenterContext();
            db.Clients.Load();
            Clients = db.Clients.Local.ToObservableCollection();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
