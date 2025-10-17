using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Nash_Decor.ModelsDB;
using System.Linq;

namespace Nash_Decor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadProducts();
        }
        private void LoadProducts()
        {
            try
            {
                using (var context = new NashDecorContext())
                {
                    
                    var products = context.Products
                        .Include(p => p.ProductType)
                        .ToList();

                    listBox.ItemsSource = products;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке продукции: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}