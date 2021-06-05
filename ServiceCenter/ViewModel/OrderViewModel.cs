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

    public class OrderViewModel : INotifyPropertyChanged
    {
        ServiceCenterContext db;
        RelayCommand addCommand;
        RelayCommand deleteCommand;
        Order selectedOrder;
        ObservableCollection<Order> orders;

        public ObservableCollection<Order> Orders
        {
            get => orders;
            set
            {
                orders = value;
                OnPropertyChanged("Orders");
            }
        }

        public Order SelectedOrder
        {
            get => selectedOrder;
            set
            {
                selectedOrder = value;
                OnPropertyChanged("SelectedOrder");
            }
        }

        public OrderViewModel()
        {
            db = new ServiceCenterContext();
            db.Orders.Load();
            Orders = db.Orders.Local.ToObservableCollection();
        }

        public void Update()
        {
            using (ServiceCenterContext context = new ServiceCenterContext())
            {
                context.Entry(SelectedOrder).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
