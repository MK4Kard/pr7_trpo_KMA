using System;
using System.Collections;
using System.Collections.Generic;
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
    /// Логика взаимодействия для PageCreatePacient.xaml
    /// </summary>
    public partial class PageCreatePacient : Page
    {
        Random rnd = new Random();
        public static List<int> listP = new List<int>();
        private Pacient currentP = new Pacient();
        Doctor doctor;

        public PageCreatePacient(Doctor doc)
        {
            InitializeComponent();
            LoadId();
            doctor = doc;
            DataContext = currentP;
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

        private void LoadId()
        {
            string jsonP = File.ReadAllText("pacId.json");
            listP = JsonSerializer.Deserialize<List<int>>(jsonP);
        }

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            if (!(String.IsNullOrEmpty(currentP.NameP) || String.IsNullOrEmpty(currentP.LastNameP) || String.IsNullOrEmpty(currentP.MiddleNameP)))
            {

                int rndD = 0;

                do
                {
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
                SaveId();
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Ошибка");
            }
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageDoctor(doctor));
        }
    }
}
