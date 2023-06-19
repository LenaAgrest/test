using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для part_three_addedit.xaml
    /// </summary>
    public partial class part_three_addedit : Page
    {
        private orders/*(название твоей таблицы)*/ _currentorder = new orders();
        public part_three_addedit(selectedPOS orders/*(твоя таблица)*/)
        {
            InitializeComponent();
            DataContext = _currentorder = new orders();
            if (selectedPOS != null)
                _currentorder = selectedPOS;
            DataContext = _currentorder;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            ///СТАВЬ В БД ВЕЗДЕ NVARCHAR И ПРОБЛЕМ НЕ БУДЕТ



            if (string.IsNullOrWhiteSpace(_currentorder.название столбца)) /*сколько у тебя полей ввода столько и этих блоков со столбцами*/
            {
                errors.AppendLine("Укажите и тут название столбца - это сообщение об ошибке");
            }

            if (string.IsNullOrWhiteSpace(_currentorder.order_description))
            {
                errors.AppendLine("Укажите описание");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentorder.order_id == 0)
            {
                // Получение максимального значения order_id из таблицы request_orders
                int maxOrderId = BuildEntities1.GetContext().order_requests.Max(o => o.request_id);

                // Увеличение order_id на 1 и добавление записи в таблицу orders
                _currentorder.order_id = maxOrderId + 1;
                BuildEntities1.GetContext().orders.Add(_currentorder);
            }

            try
            {
                BuildEntities1.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены!");
            }
            catch (DbUpdateException ex)
            {
                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    if (innerException is SqlException sqlException)
                    {
                        MessageBox.Show($"SQL exception occurred: {sqlException.Message}");
                        break;
                    }
                    innerException = innerException.InnerException;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}

