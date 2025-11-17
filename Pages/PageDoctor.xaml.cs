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
        public static List<int> listId = new List<int>();
        public static List<int> listP = new List<int>();

        public PageDoctor(Doctor doc)
        {
            LoadId();
            foreach (var id in listP)
            {
                string path = $"Pacients/P_{id}.json";

                string json = File.ReadAllText(path);

                Pacient? restored = JsonSerializer.Deserialize<Pacient>(json);

                Pacients.Add(restored);
            }
            doctor = doc;
            InitializeComponent();
            DataContext = this;
            docInfo.DataContext = doc;
            Update();
        }

        private void LoadId()
        {
            string json = File.ReadAllText("docId.json");
            listId = JsonSerializer.Deserialize<List<int>>(json);
            string jsonP = File.ReadAllText("pacId.json");
            listP = JsonSerializer.Deserialize<List<int>>(jsonP);
        }

        private void Update()
        {
            var countD = listId.Count;
            var countP = listP.Count;
            var count = countD + countP;

            Count.Content = count.ToString();
            CountD.Content = countD.ToString();
            CountP.Content = countP.ToString();
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
    }
}
