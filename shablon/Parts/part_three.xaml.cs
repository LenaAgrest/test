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
    /// Логика взаимодействия для part_three.xaml
    /// </summary>
    public partial class part_three : Page
    {
        public part_three()
        {
            InitializeComponent();
            basa.ItemsSource = BuildEntities1/*(названиебдтвойбалят*/).GetContext().orders/*(твоя таблица)*/.ToList();
            //дальше вниз смотри и разберешься как изменить
        }
        private void edit_Click(object sender, RoutedEventArgs e)
        {
            // При нажатии кнопки "edit" навигируемся на страницу AddEditOrder
            // и передаем выбранный элемент orders в качестве данных контекста
            Manager.MainFrame.Navigate(new part_three_addedit/*(название страницы с редактированием)*/((sender as System.Windows.Controls.Button).DataContext as orders));
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранные элементы orders из списка basa и преобразуем их в список
            var sotrforRemoving = basa.SelectedItems.Cast<orders>().ToList();

            // Проверяем, хочет ли пользователь удалить выбранные элементы
            if (MessageBox.Show($"Вы точно хотите удалить следующие {sotrforRemoving.Count()} элементов ?", "Bнимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    // Удаляем выбранные элементы из базы данных
                    BuildEntities1.GetContext().orders.RemoveRange(sotrforRemoving);

                    // Сохраняем изменения
                    BuildEntities1.GetContext().SaveChanges();

                    // Выводим сообщение об успешном удалении данных
                    MessageBox.Show("Данные удалены!");

                    // Обновляем источник данных списка basa, чтобы отобразить изменения
                    basa.ItemsSource = BuildEntities1.GetContext().orders.ToList();
                }
                catch (Exception ex)
                {
                    // В случае ошибки выводим сообщение об ошибке
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // При нажатии кнопки "Add" переходим на страницу part_three_addedit
            // и передаем null в качестве данных контекста (для создания нового элемента)
            Manager.MainFrame.Navigate(new part_three_addedit(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                // При изменении видимости страницы выполняем следующие действия:

                // Обновляем все записи в ChangeTracker, чтобы получить актуальные данные из базы данных
                BuildEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());

                // Устанавливаем источник данных для списка basa равным списку orders из базы данных
                basa.ItemsSource = BuildEntities1.GetContext().orders.ToList();
            }
            catch (Exception ex)
            {
                // Если происходит ошибка, ничего не делаем и оставляем блок catch пустым
            }
        }
    }
}