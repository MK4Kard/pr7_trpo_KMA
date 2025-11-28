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
    /// Логика взаимодействия для PageEnter.xaml
    /// </summary>
    public partial class PageEnter : Page
    {
        public static List<int> listId = new List<int>();

        private Login current = new Login();

        public PageEnter()
        {
            InitializeComponent();
            LoadId();
            DataContext = current;
        }

        private void LoadId()
        {
            string json = File.ReadAllText("docId.json");
            listId = JsonSerializer.Deserialize<List<int>>(json);
        }

        private void ButtonEnter(object sender, RoutedEventArgs e)
        {
            string path = $"Doctors/D_{current.Id.ToString()}.json";

            if (!File.Exists(path))
            {
                MessageBox.Show($"Пользователя с id {current.Id.ToString()} не существует");
                return;
            }

            string json = File.ReadAllText(path);

            Doctor? restoredD = JsonSerializer.Deserialize<Doctor>(json);

            if (current.Password != restoredD.Password)
            {
                MessageBox.Show("Неверный пароль");
                return;
            }

            NavigationService.Navigate(new PageDoctor(restoredD));
        }

        private void EnterReg(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration());
        }
    }

    public class Login
    {
        public int? Id { get; set; } = null;
        public string? Password { get; set; } = "";
    }
}
