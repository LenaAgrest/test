using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace shablon
{
    /// <summary>
    /// Логика взаимодействия для menu.xaml
    /// </summary>
    public partial class menu : Page
    {
        public menu()
        {
            InitializeComponent();
        }


        private void part_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new part_one());
        }

        private void part2_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new part_two());
        }

        private void part3_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new part_three());
        }
    }
}
