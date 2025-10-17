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

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var productForm = new ProductFormWindow();
            productForm.Owner = this;
            productForm.ProductSaved += (s, args) => LoadProducts(); 

            if (productForm.ShowDialog() == true)
            {
                statusText.Text = "Продукт успешно добавлен";
            }
        }
        private void ProductItem_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
            var border = sender as Border;
            if (border?.DataContext is Product selectedProduct)
            {
                
                var productForm = new ProductFormWindow(selectedProduct);
                productForm.Owner = this;
                productForm.ProductSaved += (s, args) => LoadProducts(); 

                if (productForm.ShowDialog() == true)
                {
                    statusText.Text = "Продукт успешно обновлен";
                }

            }
        }
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}