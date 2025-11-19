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
    /// Логика взаимодействия для PageChange.xaml
    /// </summary>
    public partial class PageChange : Page
    {
        private Pacient currentP;

        public PageChange(Pacient pac)
        {
            InitializeComponent();
            currentP = pac;
            DataContext = currentP;
        }

        private void ButtonSave(object sender, RoutedEventArgs e)
        {
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
                restored.PhoneNumber = currentP.PhoneNumber;

                string jsonP = JsonSerializer.Serialize<Pacient>(restored, options);

                File.WriteAllText(path, jsonP);

                MessageBox.Show($"Информация о пациенте изменена");
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Ошибка");
            }
        }

        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            string path = $"Pacients/P_{currentP.IdP}.json";
            string json = File.ReadAllText(path);

            Pacient? restored = JsonSerializer.Deserialize<Pacient>(json);

            currentP.NameP = restored.NameP;
            currentP.LastNameP = restored.LastNameP;
            currentP.MiddleNameP = restored.MiddleNameP;
            currentP.BirthDay = restored.BirthDay;
            currentP.PhoneNumber = restored.PhoneNumber;
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
