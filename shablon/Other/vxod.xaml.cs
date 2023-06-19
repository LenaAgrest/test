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
    /// Логика взаимодействия для vxod.xaml
    /// </summary>
    public partial class vxod : Page
    {
        public vxod()
        {
            InitializeComponent();
        }
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            // При выборе ToggleButton (отметке) выполняются следующие действия:

            // Присваиваем текстовому блоку passwordTextBlock значение пароля из поля ввода passtxtbx
            passwordTextBlock.Text = passtxtbx.Password;

            // Скрываем поле ввода passtxtbx
            passtxtbx.Visibility = Visibility.Hidden;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            // При снятии отметки с ToggleButton выполняются следующие действия:

            // Очищаем текстовый блок passwordTextBlock
            passwordTextBlock.Text = "";

            // Отображаем поле ввода passtxtbx
            passtxtbx.Visibility = Visibility.Visible;
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            // При нажатии кнопки "Enter" выполняются следующие действия:

            // Создаем экземпляр базы данных
            using (var db = new ТВОЕНАЗВАНИЕETENITIES())
            {
                // Ищем пользователей в базе данных, у которых совпадает логин и пароль с введенными значениями
                var users = db.sotr.Where(u => u.login == logintxtbx.Text && u.password == passtxtbx.Password).ToList();

                // Если найдены пользователи, выполняем действия
                if (users.Any())
                {
                    foreach (var user in users)
                    {
                        // В зависимости от значения поля "position" пользователя, выполняем различные действия

                        if (user.position == "ТИП ПОЛЬЗОВАТЕЛЯ КОТОРЫЙ БУДЕТ В БД")
                        {
                            // Переходим на страницу Menu
                            Manager.MainFrame.Navigate(new Menu());

                            // Если отмечено Remember, сохраняем введенные данные в настройки приложения
                            if (Remember.IsChecked == true)
                            {
                                Properties.Settings.Default.Username = logintxtbx.Text;
                                Properties.Settings.Default.Password = passtxtbx.Password;
                                Properties.Settings.Default.RememberMe = true;
                            }
                            else
                            {
                                // Если Remember не отмечено, очищаем сохраненные данные и поле ввода логина
                                Properties.Settings.Default.Username = "";
                                Properties.Settings.Default.Password = "";
                                Properties.Settings.Default.RememberMe = false;
                                logintxtbx.Text = "";
                            }
                        }
                        else if (user.position == "ТИП ПОЛЬЗОВАТЕЛЯ КОТОРЫЙ БУДЕТ В БД")
                        {
                            // Переходим на страницу Menu
                            Manager.MainFrame.Navigate(new Menu());

                            // Если отмечено Remember, сохраняем введенные данные в настройки приложения
                            if (Remember.IsChecked == true)
                            {
                                Properties.Settings.Default.Username = logintxtbx.Text;
                                Properties.Settings.Default.Password = passtxtbx.Password;
                                Properties.Settings.Default.RememberMe = true;
                            }
                            else
                            {
                                // Если Remember не отмечено, очищаем сохраненные данные и поле ввода логина
                                Properties.Settings.Default.Username = "";
                                Properties.Settings.Default.Password = "";
                                Properties.Settings.Default.RememberMe = false;
                                logintxtbx.Text = "";
                            }
                        }
                    }

                    // Сохраняем изменения в настройках приложения
                    Properties.Settings.Default.Save();
                }
                else
                {
                    // Если не найдены пользователи, выводим сообщение об ошибке
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // При загрузке страницы выполняются следующие действия:

            // Если RememberMe установлено в true, устанавливаем текстовое поле логина и поле пароля равными сохраненным значениям
            if (Properties.Settings.Default.RememberMe)
            {
                logintxtbx.Text = Properties.Settings.Default.Username;
                passtxtbx.Password = Properties.Settings.Default.Password;
                Remember.IsChecked = true;
            }
        }
    }
}
