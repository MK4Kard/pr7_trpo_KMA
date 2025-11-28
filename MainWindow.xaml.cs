using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using pr7_trpo_1_KMA.Pages;

namespace pr7_trpo_1_KMA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new PageEnter());
        }

        private void ChangeTheme_Click(object sender, RoutedEventArgs e)
        {
            ThemeHelper.Toggle();
        }
    }

    public class Doctor : INotifyPropertyChanged
    {
        private int _id = 12345;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _name = "";
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _lastName = "";
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        private string _middleName = "";
        public string MiddleName
        {
            get => _middleName;
            set
            {
                _middleName = value;
                OnPropertyChanged();
            }
        }

        private string _specialisation = "";
        public string Specialisation
        {
            get => _specialisation;
            set
            {
                _specialisation = value;
                OnPropertyChanged();
            }
        }

        private string _pass = "";
        public string Password
        {
            get => _pass;
            set
            {
                _pass = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

    public class Pacient : INotifyPropertyChanged
    {
        private int _id = 1234567;
        public int IdP
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _name = "";
        public string NameP
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _lastName = "";
        public string LastNameP
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        private string _middleName = "";
        public string MiddleNameP
        {
            get => _middleName;
            set
            {
                _middleName = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _birthday;
        public DateTime? BirthDay
        {
            get => _birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }
        
        private long? _phoneNumber;
        public long? PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Appointment>? appointment;
        public ObservableCollection<Appointment>? Appointments
        {
            get => appointment;
            set
            {
                appointment = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

    public class Appointment()
    {
        private DateTime date = new DateTime(1990, 01, 01);
        private int doctor_id = 12345;
        private string diagnosis = "";
        private string recomendations = "";
        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
            }
        }

        public int DocId
        {
            get => doctor_id;
            set
            {
                doctor_id = value;
            }
        }

        public string Diagnosis
        {
            get => diagnosis;
            set
            {
                diagnosis = value;
            }
        }

        public string Recomendations
        {
            get => recomendations;
            set
            {
                recomendations = value;
            }
        }
    }
}