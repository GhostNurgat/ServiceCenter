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
    using System.Windows;
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

        private string surname;
        public string Surname
        {
            get => surname;
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }

        public ClientViewModel()
        {
            db = new ServiceCenterContext();
            db.Clients.Load();
            Clients = db.Clients.Local.ToObservableCollection();
        }

        public RelayCommand AddCommand
        {
            get => addCommand ??
                (addCommand = new RelayCommand(obj =>
                {
                    AddClientWindow addClient = new AddClientWindow(new Client());
                    try
                    {
                        if (addClient.ShowDialog() == true)
                        {
                            Client client = addClient.Client;
                            db.Clients.Add(client);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Произошла ошибка: Данные не сохранены, т.к. все поля были пустые.",
                            "Обновление база данных", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }));
        }

        public RelayCommand EditCommand
        {
            get => editCommand ??
                (editCommand = new RelayCommand(selectedItem =>
                {
                    if (selectedItem == null) return;
                    Client client = selectedItem as Client;

                    Client temp = new Client()
                    {
                        ClientId = client.ClientId,
                        Surname = client.Surname,
                        Name = client.Name,
                        Patronymic = client.Patronymic,
                        Birthday = client.Birthday,
                        Address = client.Address,
                        Phone = client.Phone,
                        Email = client.Email
                    };
                    EditClientWindow editClient = new EditClientWindow(temp);

                    try
                    {
                        if (editClient.ShowDialog() == true)
                        {
                            client = db.Clients.Find(editClient.Client.ClientId);
                            if (client != null)
                            {
                                client.Surname = editClient.Client.Surname;
                                client.Name = editClient.Client.Name;
                                client.Patronymic = editClient.Client.Patronymic;
                                client.Birthday = editClient.Client.Birthday;
                                client.Address = editClient.Client.Address;
                                client.Phone = editClient.Client.Phone;
                                client.Email = editClient.Client.Email;

                                db.Entry(client).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Произошла ошибка: Данные не сохранены, т.к. все поля были пустые.",
                            "Обновление база данных", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }, (obj) => Clients.Count > 0));
        }

        public RelayCommand DeleteCommand
        {
            get => deleteCommand ??
                (deleteCommand = new RelayCommand(obj =>
                {
                    var result = MessageBox.Show("Вы уверены, что хотите удалить клиента? После удаления, нет возможности восстановление данных.", "Удаление клиента",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result != MessageBoxResult.Yes) return;

                    Client client = obj as Client;
                    if (client != null)
                    {
                        using (ServiceCenterContext context = new ServiceCenterContext())
                        {
                            List<Client> temp = context.Clients.ToList();
                            var selectedItem = from t in context.Clients
                                               where client.ClientId == t.ClientId
                                               select t;
                            context.Clients.Remove(selectedItem.FirstOrDefault());

                            context.SaveChanges();
                        }

                        db.Clients.Remove(client);
                    }
                }, (obj) => Clients.Count > 0));
        }

        public void SearchBySurname()
        {
            Clients = db.Clients
                .Where(c => c.Surname.Contains(Surname))
                .ToObservableCollection();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
