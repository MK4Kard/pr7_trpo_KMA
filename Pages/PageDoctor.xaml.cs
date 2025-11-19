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
    /// Логика взаимодействия для PageDoctor.xaml
    /// </summary>
    public partial class PageDoctor : Page
    {
        public ObservableCollection<Pacient> Pacients { get; set; } = new();
        public Pacient? SelectedPacient { get; set; }
        private Doctor doctor;
        private Count count = new Count();
        public static List<int> listId = new List<int>();
        public static List<int> listP = new List<int>();

        public PageDoctor(Doctor d)
        {
            LoadId();
            foreach (var id in listP)
            {
                string path = $"Pacients/P_{id}.json";

                string json = File.ReadAllText(path);

                Pacient? restored = JsonSerializer.Deserialize<Pacient>(json);

                Pacients.Add(restored);
            }
            doctor = d;
            InitializeComponent();
            DataContext = this;
            docInfo.DataContext = doctor;
            info.DataContext = count;
            count.CountD = listId.Count;
            count.CountP = listP.Count;
            count.CountAll = listId.Count + listP.Count;
        }

        private void LoadId()
        {
            string json = File.ReadAllText("docId.json");
            listId = JsonSerializer.Deserialize<List<int>>(json);
            string jsonP = File.ReadAllText("pacId.json");
            listP = JsonSerializer.Deserialize<List<int>>(jsonP);
        }

        private void SaveId()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string jsonP = JsonSerializer.Serialize(listP, options);
            File.WriteAllText("pacId.json", jsonP);
        }

        private void CreatePac(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageCreatePacient(doctor));
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            if (SelectedPacient == null)
            {
                MessageBox.Show("Пациент не выбран");
                return;
            }

            NavigationService.Navigate(new PageAppointment(doctor, SelectedPacient));
        }

        private void RePacient(object sender, RoutedEventArgs e)
        {
            if (SelectedPacient == null)
            {
                MessageBox.Show("Пациент не выбран");
                return;
            }

            NavigationService.Navigate(new PageChange(SelectedPacient));
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (SelectedPacient == null)
            {
                MessageBox.Show("Пациент не выбран");
                return;
            }

            listP.Remove(SelectedPacient.IdP);
            string path = $"Pacients/P_{SelectedPacient.IdP}.json";
            File.Delete(path);
            Pacients.Remove(SelectedPacient);

            SaveId();
        }
    }

    public class Count()
    {
        private int _countD = 0;
        public int CountD
        {
            get => _countD;
            set
            {
                _countD = value;
            }
        }
        private int _countP = 0;
        public int CountP
        {
            get => _countP;
            set
            {
                _countP = value;
            }
        }

        private int _countAll = 0 + 0;
        public int CountAll
        {
            get => _countAll;
            set
            {
                _countAll = value;
            }
        }
    }
}
