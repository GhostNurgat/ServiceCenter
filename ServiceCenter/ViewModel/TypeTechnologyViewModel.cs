using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ServiceCenter.ViewModel
{
    using ServiceCenter.Models;
    using ServiceCenter.View;
    using ServiceCenter.Extensions;

    public class TypeTechnologyViewModel : INotifyPropertyChanged
    {
        ServiceCenterContext db;
        RelayCommand addCommand;
        RelayCommand removeCommand;
        ObservableCollection<TypeTechnology> typeTechnologies;

        public ObservableCollection<TypeTechnology> TypeTechnologies
        {
            get => typeTechnologies;
            set
            {
                typeTechnologies = value;
                OnPropertyChanged("TypeTechnologies");
            }
        }

        public TypeTechnologyViewModel()
        {
            db = new ServiceCenterContext();
            db.TypeTechnologies.Load();
            TypeTechnologies = db.TypeTechnologies.Local.ToObservableCollection();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
