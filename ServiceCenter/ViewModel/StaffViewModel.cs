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
    public class StaffViewModel : INotifyPropertyChanged
    {
        ServiceCenterContext db;
        RelayCommand addCommand;
        RelayCommand removeCommand;
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



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
