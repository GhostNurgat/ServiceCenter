using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.Entity;

namespace ServiceCenter.ViewModel
{
    using ServiceCenter.Models;
    using ServiceCenter.Extensions;

    public class PositionViewModel : INotifyPropertyChanged
    {
        ServiceCenterContext db;
        ObservableCollection<Position> positions;

        public ObservableCollection<Position> Positions
        {
            get => positions;
            set
            {
                positions = value;
                OnPropertyChanged("Positions");
            }
        }

        public PositionViewModel()
        {
            db = new ServiceCenterContext();
            db.Positions.Load();
            Positions = db.Positions.Local.ToObservableCollection();
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
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
