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
    using System.Data.Entity.Infrastructure;
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

                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Произошла ошибка: Данные не сохранены, т.к. все поля были пустые.", 
                                "Обновление база данных", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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
                        PositionId = staffMember.PositionId,
                        Phone = staffMember.Phone
                    };
                    EditStaffWindow editStaff = new EditStaffWindow(staff);

                    try
                    {
                        if (editStaff.ShowDialog() == true)
                        {
                            staffMember = db.StaffMembers.Find(editStaff.StaffMember.StaffId);
                            if (staffMember != null)
                            {
                                staffMember.Surname = editStaff.StaffMember.Surname;
                                staffMember.Name = editStaff.StaffMember.Name;
                                staffMember.Patronymic = editStaff.StaffMember.Patronymic;
                                staffMember.PositionId = editStaff.StaffMember.PositionId;
                                staffMember.Phone = editStaff.StaffMember.Phone;

                                db.Entry(staffMember).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Произошла ошибка: Данные не сохранены, т.к. все поля были пустые.",
                                "Обновление база данных", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }, (obj) => StaffMembers.Count > 0));
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

        public void SearchBySurname()
        {
            StaffMembers = db.StaffMembers
                .Where(s => s.Surname.Contains(Surname))
                .ToObservableCollection();
        }

        public void FilterPosition(string selectedItem)
        {
            StaffMembers = db.StaffMembers
                .Where(s => s.Position.Name.Contains(selectedItem))
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
