using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_CalendarSelectedDatesChanged.Commands;

namespace WPF_CalendarSelectedDatesChanged
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ICommand _changePasswdCommand;
        private ICommand _changeDatesCommand;
        private ICommand _showDatesCommand;

        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<DateTime> _dates;
        private string _passwd;

        public ObservableCollection<DateTime> Dates 
        { 
            get
            {
                return _dates ?? (_dates = new ObservableCollection<DateTime>());
            }
        }
        public string Passwd 
        { 
            get
            {
                return _passwd;
            }
            set
            {
                _passwd = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Passwd)));
            }
        }

        public ICommand ChangePasswdCommand { get => _changePasswdCommand ??= new RelayCommand<string>((passwd) => Passwd = passwd); }
        public ICommand ChangeDatesCommand { get => _changeDatesCommand ??= new RelayCommand<IEnumerable<DateTime>>(OnDateChanged); }
        public ICommand ShowDatesCommand { get => _showDatesCommand ??= new RelayCommand(OpenDisplayDates); }

        private void OpenDisplayDates()
        {
            DisplayDatesWindows displayDatesWindows = new DisplayDatesWindows();
            displayDatesWindows.ShowDialog();
        }

        private void OnDateChanged(IEnumerable<DateTime> dates)
        {
            Dates.Clear();

            foreach(DateTime dateTime in Dates)
            {
                Application.Current.Dispatcher.Invoke(() => Dates.Add(dateTime));                
            }
        }
    }
}
