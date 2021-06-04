using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ServiceCenter.Models;
using System.Collections.ObjectModel;
using System.Data.Entity;
using ServiceCenter.Extensions;

namespace ServiceCenter.ViewModel
{
    using ServiceCenter.View;
    using System.Windows;

    public class StaffViewModel : INotifyPropertyChanged
    {
        ServiceCenterContext db;
        RelayCommand addCommand;
        RelayCommand removeCommand;
        RelayCommand editCommand;
        ObservableCollection<StaffMember> staffMembers;

        public ObservableCollection<StaffMember> StaffMembers
        {
            get => staffMembers;
            set
            {
                staffMembers = value;
                OnPropertyChanged("StaffMembers");
            }
        }

        public StaffViewModel()
        {
            db = new ServiceCenterContext();
            db.StaffMembers.Load();
            StaffMembers = db.StaffMembers.Local.ToObservableCollection();
        }

        public RelayCommand AddCommand
        {
            get => addCommand ??
                (addCommand = new RelayCommand((obj) =>
                {
                    AddStaffWindow addStaff = new AddStaffWindow(new StaffMember());
                    if (addStaff.ShowDialog() == true)
                    {
                        StaffMember member = addStaff.StaffMember;
                        db.StaffMembers.Add(member);
                        db.SaveChanges();
                    }
                }));
        }

        public RelayCommand EditCommand
        {
            get => editCommand ??
                (editCommand = new RelayCommand((selectedItem) =>
                {
                    if (selectedItem == null) return;
                    StaffMember staffMember = selectedItem as StaffMember;

                    StaffMember staff = new StaffMember()
                    {
                        StaffId = staffMember.StaffId,
                        Surname = staffMember.Surname,
                        Name = staffMember.Name,
                        Patronymic = staffMember.Patronymic,
                        Position = staffMember.Position,
                        Phone = staffMember.Phone
                    };
                    EditStaffWindow editStaff = new EditStaffWindow(staff);

                    if (editStaff.ShowDialog() == true)
                    {
                        staffMember = db.StaffMembers.Find(editStaff.StaffMember.StaffId);
                        if (staffMember != null)
                        {
                            staffMember.Surname = editStaff.StaffMember.Surname;
                            staffMember.Name = editStaff.StaffMember.Name;
                            staffMember.Patronymic = editStaff.StaffMember.Patronymic;
                            staffMember.Position = editStaff.StaffMember.Position;
                            staffMember.Phone = editStaff.StaffMember.Phone;

                            db.Entry(staffMember).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }));
        }

        public RelayCommand RemoveCommand
        {
            get => removeCommand ??
                (removeCommand = new RelayCommand((obj) =>
                {
                    var result = MessageBox.Show("Вы уверены, что хотите удалить сотрудника? После удаления, нет возможности восстановление данных.",
                        "Удаление сотрудника", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result != MessageBoxResult.Yes) return;

                    StaffMember staff = obj as StaffMember;
                    if (staff != null)
                    {
                        using (ServiceCenterContext context = new ServiceCenterContext())
                        {
                            List<StaffMember> temp = context.StaffMembers.ToList();
                            var selectedItem = from t in context.StaffMembers
                                               where staff.StaffId == t.StaffId
                                               select t;
                            context.StaffMembers.Remove(selectedItem.FirstOrDefault());

                            context.SaveChanges();
                        }

                        db.StaffMembers.Remove(staff);
                    }
                }, (obj) => StaffMembers.Count > 0));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
