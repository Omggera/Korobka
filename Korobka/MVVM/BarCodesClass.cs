using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Korobka.MVVM
{
    internal class BarCodesClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<string> barCodes;

        public ObservableCollection<string> BarCodes
        {
            get => barCodes;
            set
            {
                if(barCodes != value)
                {
                    barCodes = value;
                    OnPropertyChanged();
                }
            }
        }

        public BarCodesClass()
        {
            this.barCodes = BarCodes;
        //    //barCodes.Add("value");
        //    //barCodes.Add("2");
        //    //barCodes.Add("3");
        //    //barCodes.Add("4");

        }

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public void Adde()
        {
            BarCodes.Add("1");
            BarCodes.Add("2");
            BarCodes.Add("3");
        }
    }
}
