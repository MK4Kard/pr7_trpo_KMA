using System;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        Random rnd = new Random();
        public static List<int> listId = new List<int>();
        private Doctor currentD = new Doctor();

        public Registration()
        {
            InitializeComponent();
            LoadId();
            DataContext = currentD;
        }
        private void SaveId()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(listId, options);
            File.WriteAllText("docId.json", json);
        }

        private void LoadId()
        {
            string json = File.ReadAllText("docId.json");
            listId = JsonSerializer.Deserialize<List<int>>(json);
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (!(String.IsNullOrEmpty(currentD.Name) || String.IsNullOrEmpty(currentD.LastName) || String.IsNullOrEmpty(currentD.MiddleName) || String.IsNullOrEmpty(currentD.Specialisation) || String.IsNullOrEmpty(currentD.Password) || String.IsNullOrEmpty(rePass.Text)))
            {
                if (currentD.Password != rePass.Text)
                {
                    MessageBox.Show($"Пароли не совпадают");
                    return;
                }

                int rndD = 0;

                do
                {
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

        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
