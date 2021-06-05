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
        RelayCommand editCommand;
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

        public RelayCommand AddCommand
        {
            get => addCommand ??
                (addCommand = new RelayCommand(obj =>
                {
                    Order temp = new Order()
                    {
                        OrderId = Orders.Last().OrderId + 1
                    };
                    AddOrderWindow addOrder = new AddOrderWindow(temp);
                    if (addOrder.ShowDialog() == true)
                    {
                        Order order = addOrder.Order;
                        db.Orders.Add(order);
                        db.SaveChanges();
                    }
                }));
        }

        public RelayCommand EditCommand
        {
            get => editCommand ??
                (editCommand = new RelayCommand(selectedItem =>
                {
                    if (selectedItem == null) return;
                    Order order = selectedItem as Order;

                    Order temp = new Order()
                    {
                        OrderId = order.OrderId,
                        Service = order.Service,
                        Price = order.Price,
                        Guarantee = order.Guarantee,
                        StaffId = order.StaffId,
                        DateOrder = order.DateOrder,
                        Done = order.Done,
                        Payment = order.Payment
                    };
                    EditOrderWindow editOrder = new EditOrderWindow(temp);

                    if (editOrder.ShowDialog() == true)
                    {
                        order = db.Orders.Find(editOrder.Order.OrderId);
                        if (order != null)
                        {
                            order.Service = editOrder.Order.Service;
                            order.Price = editOrder.Order.Price;
                            order.Guarantee = editOrder.Order.Guarantee;
                            order.StaffId = editOrder.Order.StaffId;
                            order.DateOrder = editOrder.Order.DateOrder;
                            order.Done = editOrder.Order.Done;
                            order.Payment = editOrder.Order.Payment;

                            db.Entry(order).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }));
        }

        public RelayCommand DeleteCommand
        {
            get => deleteCommand ??
                (deleteCommand = new RelayCommand(obj =>
                {
                    var result = MessageBox.Show("Вы уверены, что хотите удалить заказ?" +
                        "После удаления, нет возможности восстановление данных.", "Удаление заказа",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result != MessageBoxResult.Yes) return;

                    Order order = obj as Order;
                    if (order != null)
                    {
                        using (ServiceCenterContext context = new ServiceCenterContext())
                        {
                            List<Order> temp = context.Orders.ToList();
                            var selectedItem = from t in context.Orders
                                               where order.OrderId == t.OrderId
                                               select t;
                            context.Orders.Remove(selectedItem.FirstOrDefault());

                            context.SaveChanges();
                        }

                        db.Orders.Remove(order);
                    }
                }, (obj) => Orders.Count > 0));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
