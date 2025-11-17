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

namespace pr7_trpo_1_KMA.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAppointment.xaml
    /// </summary>
    public partial class PageAppointment : Page
    {
        public ObservableCollection<Appointment> Apps { get; set; } = new();
        private Doctor doctor;
        private Pacient pacient;
        private Appointment apps = new Appointment();
        public PageAppointment(Doctor doc, Pacient pac)
        {
            InitializeComponent();
            docPac.DataContext = apps;
            doctor = doc;
            pacient = pac;
            pacientInfo.DataContext = pacient;
            if (pacient.Appointments != null)
            {
                foreach (var a in pacient.Appointments)
                {
                    Apps.Add(a);
                }
            }
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
    }
}
