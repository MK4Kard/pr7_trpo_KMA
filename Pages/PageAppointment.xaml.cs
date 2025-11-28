using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.Azure.Amqp.Serialization.SerializableType;

namespace pr7_trpo_1_KMA.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAppointment.xaml
    /// </summary>
    public partial class PageAppointment : Page
    {
        public ObservableCollection<Appointment> Apps { get; set; } = new();
        public Appointment? SelectedAppointment { get; set; }
        private Doctor doctor;
        private Pacient pacient;
        private Appointment apps = new Appointment();
        private Ages ages = new Ages();
        private AppDate ad = new AppDate();
        public PageAppointment(Doctor doc, Pacient pac)
        {
            InitializeComponent();
            docPac.DataContext = apps;
            doctor = doc;
            pacient = pac;
            ages.Day = pacient.BirthDay;
            age.DataContext = ages;
            pacientInfo.DataContext = pacient;
            if (pacient.Appointments != null)
            {
                foreach (var a in pacient.Appointments)
                {
                    Apps.Add(a);
                }
                ad.App = pacient.Appointments.Last();
            }
            appd.DataContext = ad;
            DataContext = this;
        }

        private void ButtonSave(object sender, RoutedEventArgs e)
        {
            var app = (Appointment)docPac.DataContext;
            string path = $"Pacients/P_{pacient.IdP}.json";
            string json = File.ReadAllText(path);

            Pacient? restored = JsonSerializer.Deserialize<Pacient>(json);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            Appointment pp = new Appointment
            {
                Date = DateTime.Now,
                DocId = doctor.Id,
                Diagnosis = app.Diagnosis,
                Recomendations = app.Recomendations
            };

            Apps.Add(pp);

            restored.Appointments = Apps;

            string jsonP = JsonSerializer.Serialize<Pacient>(restored, options);

            File.WriteAllText(path, jsonP);

            MessageBox.Show("Информация сохранена");
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (SelectedAppointment == null)
            {
                MessageBox.Show("Пациент не выбран");
                return;
            }

            Apps.Remove(SelectedAppointment);


            string path = $"Pacients/P_{pacient.IdP}.json";
            string json = File.ReadAllText(path);

            Pacient? restored = JsonSerializer.Deserialize<Pacient>(json);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            restored.Appointments = Apps;

            string jsonP = JsonSerializer.Serialize<Pacient>(restored, options);

            File.WriteAllText(path, jsonP);
        }
    }

    public class Ages
    {
        public DateTime? Day { get; set; }
        private int age = 0;
        public int Age
        {
            get
            {
                if (Day == null)
                    return 0;

                var bd = Day.Value;
                age = DateTime.Now.Year - bd.Year;

                return age;
            }
        }

        private string coming = "";
        public string Coming
        {
            get
            {
                if (age >= 18)
                    coming = "совершеннолетний";
                else
                    coming = "несовершеннолетний";

                return coming;
            }
        }
    }

    public class AppDate
    {
        public Appointment? App { get; set; }

        private string date = "";
        public string Date
        {
            get
            {
                if (App != null)
                {
                    var days = (DateTime.Now.Date - App.Date.Date).Days;

                    date = $"Приём был {days} дней назад";
                }
                else
                {
                    date = "Первый приём";
                }

                return date;
            }
        }
    }
}
