using Microsoft.EntityFrameworkCore;
using Nash_Decor.ModelsDB;
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
using System.Windows.Shapes;

namespace Nash_Decor
{
    /// <summary>
    /// Логика взаимодействия для ProductFormWindow.xaml
    /// </summary>
    public partial class ProductFormWindow : Window
    {
        private Product _editingProduct;
        private bool _isEditMode = false;

        public event EventHandler ProductSaved;
        public ProductFormWindow()
        {
            InitializeComponent();
            LoadProductTypes();
        }
        public ProductFormWindow(Product product) : this()
        {
            _editingProduct = product;
            _isEditMode = true;
            formTitle.Text = "Редактирование продукта";
            LoadProductData();
        }

        private void LoadProductTypes()
        {
            try
            {
                using (var context = new NashDecorContext())
                {
                    var productTypes = context.ProductTypes.ToList();
                    cmbProductType.ItemsSource = productTypes;

                    if (productTypes.Any())
                        cmbProductType.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при загрузке типов продуктов: {ex.Message}");
            }
        }

        private void LoadProductData()
        {
            if (_editingProduct == null) return;

           
            txtArticle.Text = _editingProduct.Article;
            txtProductName.Text = _editingProduct.ProductName;
            txtMinPartnerPrice.Text = _editingProduct.MinPartnerPrice.ToString("F2");
            txtRollWidth.Text = _editingProduct.RollWidth.ToString("F2");

            
            if (cmbProductType.ItemsSource != null)
            {
                var productTypes = cmbProductType.ItemsSource.Cast<ProductType>();
                var selectedType = productTypes.FirstOrDefault(pt => pt.ProductTypeId == _editingProduct.ProductTypeId);
                cmbProductType.SelectedItem = selectedType;
            }

            
            txtArticle.IsEnabled = false;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                using (var context = new NashDecorContext())
                {
                    Product product;

                    if (_isEditMode)
                    {
                       
                        product = context.Products.Find(_editingProduct.ProductId);
                        if (product == null)
                        {
                            ShowErrorMessage("Продукт не найден в базе данных");
                            return;
                        }
                    }
                    else
                    {
                        
                        product = new Product();
                        context.Products.Add(product);
                    }

                    product.Article = txtArticle.Text.Trim();
                    product.ProductName = txtProductName.Text.Trim();
                    product.MinPartnerPrice = decimal.Parse(txtMinPartnerPrice.Text);
                    product.RollWidth = decimal.Parse(txtRollWidth.Text);

                    var selectedType = cmbProductType.SelectedItem as ProductType;
                    if (selectedType != null)
                    {
                        product.ProductTypeId = selectedType.ProductTypeId;
                    }

                    context.SaveChanges();

                   
                    ProductSaved?.Invoke(this, EventArgs.Empty);

                    DialogResult = true;
                    Close();
                }
            }
            catch (DbUpdateException ex)
            {
                ShowErrorMessage($"Ошибка при сохранении в базу данных: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Неожиданная ошибка: {ex.Message}");
            }
        }
        private bool ValidateInput()
        {
            HideValidation();

            
            if (string.IsNullOrWhiteSpace(txtArticle.Text))
            {
                ShowValidation("Поле 'Артикул' обязательно для заполнения");
                return false;
            }

            if (cmbProductType.SelectedItem == null)
            {
                ShowValidation("Необходимо выбрать тип продукта");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                ShowValidation("Поле 'Наименование' обязательно для заполнения");
                return false;
            }

           
            if (!decimal.TryParse(txtMinPartnerPrice.Text, out decimal price) || price < 0)
            {
                ShowValidation("Стоимость должна быть положительным числом с точностью до сотых. Пример: 1500.75");
                return false;
            }

          
            if (!decimal.TryParse(txtRollWidth.Text, out decimal width) || width < 0)
            {
                ShowValidation("Ширина должна быть положительным числом с точностью до сотых. Пример: 1.50");
                return false;
            }

            
            if (price != Math.Round(price, 2))
            {
                ShowValidation("Стоимость должна быть указана с точностью до двух знаков после запятой");
                return false;
            }

            
            if (width != Math.Round(width, 2))
            {
                ShowValidation("Ширина должна быть указана с точностью до двух знаков после запятой");
                return false;
            }

            return true;
        }

        private void ShowValidation(string message)
        {
            txtValidation.Text = message;
            validationBorder.Visibility = Visibility.Visible;
        }

        private void HideValidation()
        {
            validationBorder.Visibility = Visibility.Collapsed;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
           
            if (HasUnsavedChanges())
            {
                var result = MessageBox.Show(
                    "У вас есть несохраненные изменения. Вы уверены, что хотите выйти?",
                    "Подтверждение выхода",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                    return;
            }

            DialogResult = false;
            Close();
        }

        private bool HasUnsavedChanges()
        {
            if (_isEditMode && _editingProduct != null)
            {
                
                return txtArticle.Text != _editingProduct.Article ||
                       txtProductName.Text != _editingProduct.ProductName ||
                       decimal.Parse(txtMinPartnerPrice.Text) != _editingProduct.MinPartnerPrice ||
                       decimal.Parse(txtRollWidth.Text) != _editingProduct.RollWidth;
            }
            else
            {
                
                return !string.IsNullOrWhiteSpace(txtArticle.Text) ||
                       !string.IsNullOrWhiteSpace(txtProductName.Text) ||
                       !string.IsNullOrWhiteSpace(txtMinPartnerPrice.Text) ||
                       !string.IsNullOrWhiteSpace(txtRollWidth.Text);
            }
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(this, message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ShowInfoMessage(string message)
        {
            MessageBox.Show(this, message, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
