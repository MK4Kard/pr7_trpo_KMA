using System;
using System.ComponentModel;
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

namespace pr7_trpo_1_KMA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Doctor CurrentDoc = new Doctor();
        //private Pacient CurrentPac = new Pacient();
        Random rnd = new Random();
        List<int> listId = new List<int>();
        List<int> listP = new List<int>();
        
        public MainWindow()
        {
            InitializeComponent();
            LoadId();
            foreach (var id in listP)
            {
                pacients.Items.Add(id);
            }
        }

        private void SaveId()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(listId, options);
            File.WriteAllText("docId.json", json);
            string jsonP = JsonSerializer.Serialize(listP, options);
            File.WriteAllText("pacId.json", jsonP);
        }

        private void LoadId()
        {
            string json = File.ReadAllText("docId.json");
            listId = JsonSerializer.Deserialize<List<int>>(json);
            string jsonP = File.ReadAllText("pacId.json");
            listP = JsonSerializer.Deserialize<List<int>>(jsonP);
        }

        private void ButtonDClick(object sender, RoutedEventArgs e)
        {
            var currentD = (Doctor)Resources["CurrentUser"];

            if (!(String.IsNullOrEmpty(currentD.Name) || String.IsNullOrEmpty(currentD.LastName) || String.IsNullOrEmpty(currentD.MiddleName) || String.IsNullOrEmpty(currentD.Specialisation) || String.IsNullOrEmpty(currentD.Password) || String.IsNullOrEmpty(rePass.Text)))
            {
                if (currentD.Password != rePass.Text)
                {
                    MessageBox.Show($"Пароли не совпадают");
                    return;
                }
                
                int rndD = 0;

                do {
                    rndD = rnd.Next(10000, 100000);
                } 
                while (listId.Contains(rndD));

                listId.Add(rndD);

                string path = $"Doctors/D_{rndD}.json";
                currentD.Id = rndD;

                Doctor user = new Doctor
                {
                    Id = currentD.Id,
                    Name = currentD.Name,
                    LastName = currentD.LastName,
                    MiddleName = currentD.MiddleName,
                    Specialisation = currentD.Specialisation,
                    Password = currentD.Password
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize<Doctor>(user, options);
                File.WriteAllText(path, json);

                MessageBox.Show($"Пользователь создан");
                SaveId();
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Ошибка");
            }
        }

        private void ButtonEnter(object sender, RoutedEventArgs e)
        {           
            if (String.IsNullOrEmpty(idD.Text) && String.IsNullOrEmpty(passD.Text))
            {
                MessageBox.Show("Заполните все поля", "Ошибка");
                return;
            }
            else
            {
                string path = $"Doctors/D_{idD.Text}.json";

                if (!File.Exists(path))
                {
                    MessageBox.Show($"Пользователя с id {idD.Text} не существует");
                    return;
                }

                string json = File.ReadAllText(path);

                Doctor? restoredD = JsonSerializer.Deserialize<Doctor>(json);

                if (passD.Text != restoredD.Password)
                {
                    MessageBox.Show("Неверный пароль");
                    return;
                }

                CurrentDoc = restoredD;

                doctorInfo.IsEnabled = true;
                pacientAdd.IsEnabled = true;
                pacientsList.IsEnabled = true;
                doctorInfo.DataContext = restoredD;
            }
        }

        private void ButtonPClick(object sender, RoutedEventArgs e)
        {
            var currentP = (Pacient)Resources["CurrentPacient"];
            
            if (!(String.IsNullOrEmpty(currentP.NameP) || String.IsNullOrEmpty(currentP.LastNameP) || String.IsNullOrEmpty(currentP.MiddleNameP))) {
                
                int rndD = 0;

                do {
                    rndD = rnd.Next(1000000, 10000000);
                }
                while (listP.Contains(rndD));

                listP.Add(rndD);

                string path = $"Pacients/P_{rndD}.json";
                currentP.IdP = rndD;

                Pacient pacient = new Pacient
                {
                    IdP = currentP.IdP,
                    NameP = currentP.NameP,
                    LastNameP = currentP.LastNameP,
                    MiddleNameP = currentP.MiddleNameP,
                    BirthDay = currentP.BirthDay
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize<Pacient>(pacient, options);
                File.WriteAllText(path, json);

                MessageBox.Show($"Пациент добавлен");
                pacients.Items.Add(rndD);
                SaveId();
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Ошибка");
            }
        }

        private void ButtonPSave(object sender, RoutedEventArgs e)
        {
            var currentP = (Pacient)pacientRe.DataContext;

            if (!(String.IsNullOrEmpty(currentP.NameP) || String.IsNullOrEmpty(currentP.LastNameP) || String.IsNullOrEmpty(currentP.MiddleNameP)))
            {
                string path = $"Pacients/P_{currentP.IdP}.json";

                string json = File.ReadAllText(path);

                Pacient? restored = JsonSerializer.Deserialize<Pacient>(json);

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                restored.NameP = currentP.NameP;
                restored.LastNameP = currentP.LastNameP;
                restored.MiddleNameP = currentP.MiddleNameP;
                restored.BirthDay = currentP.BirthDay;

                string jsonP = JsonSerializer.Serialize<Pacient>(restored, options);

                File.WriteAllText(path, jsonP);

                MessageBox.Show($"Информация о пациенте изменена");
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Ошибка");
            }
        }

        private void pacients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(pacients.SelectedItem.ToString())) {

                string pac = pacients.SelectedItem.ToString();

                string path = $"Pacients/P_{pac}.json";

                if (!File.Exists(path))
                {
                    MessageBox.Show($"Пациента с id {pac} не существует");
                    return;
                }

                string json = File.ReadAllText(path);

                Pacient? restored = JsonSerializer.Deserialize<Pacient>(json);

                pacientRe.DataContext = restored;
                appointment.DataContext = restored;
                pacientRe.IsEnabled = true;
                appointment.IsEnabled = true;
            }
        }

        private void ButtonSave(object sender, RoutedEventArgs e)
        {
            var currentP = (Pacient)appointment.DataContext;
            string path = $"Pacients/P_{currentP.IdP}.json";

            if (!File.Exists(path))
            {
                MessageBox.Show($"Пациента с id {currentP.IdP} не существует");
                return;
            }

            string json = File.ReadAllText(path);

            Pacient? restored = JsonSerializer.Deserialize<Pacient>(json);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            restored.LastAppointment = currentP.LastAppointment;
            restored.LastDoctor = CurrentDoc.Id;
            restored.Diagnosis = currentP.Diagnosis;
            restored.Recomendations = currentP.Recomendations;

            string jsonP = JsonSerializer.Serialize<Pacient>(restored, options);

            File.WriteAllText(path, jsonP);

            MessageBox.Show("Информация сохранена");
        }

        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            var currentP = (Pacient)appointment.DataContext;
            string path = $"Pacients/P_{currentP.IdP}.json";

            if (!File.Exists(path))
            {
                MessageBox.Show($"Пациента с id {currentP.IdP} не существует");
                return;
            }

            string json = File.ReadAllText(path);

            Pacient? restored = JsonSerializer.Deserialize<Pacient>(json);

            currentP.Diagnosis = restored.Diagnosis;
            currentP.Recomendations = restored.Recomendations;
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

        private DateTime _birthday = new DateTime(1990, 01, 01);
        public DateTime BirthDay
        {
            get => _birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }

        // null данные до приема

        private DateTime? _lastAppointment;
        public DateTime? LastAppointment
        {
            get => _lastAppointment;
            set
            {
                _lastAppointment = value;
                OnPropertyChanged();
            }
        }

        private int? _lastDoctor;
        public int? LastDoctor
        {
            get => _lastDoctor;
            set
            {
                _lastDoctor = value;
                OnPropertyChanged();
            }
        }

        private string? _diagnosis;
        public string? Diagnosis
        {
            get => _diagnosis;
            set
            {
                _diagnosis = value;
                OnPropertyChanged();
            }
        }

        private string? _recomendations;
        public string? Recomendations
        {
            get => _recomendations;
            set
            {
                _recomendations = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}