using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace CapitalLeasing
{
    public partial class CarCatalogWindow : Window
    {
        public CarCatalogWindow()
        {
            InitializeComponent();

            // Устанавливаем фокус на поле марки при загрузке
            Loaded += (s, e) => BrandTextBox.Focus();
        }

        // Обработчик для проверки ввода только цифр в поля мощности
        private void PowerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // Обработчик изменения текста в поле марки
        private void BrandTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Плейсхолдер автоматически скрывается при вводе текста
            // Можно добавить логику поиска по мере ввода
        }

        // Обработчик изменения текста в поле минимальной мощности
        private void MinPowerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MinPowerTextBox.Text))
            {
                if (!string.IsNullOrEmpty(MaxPowerTextBox.Text))
                {
                    int minValue, maxValue;
                    if (int.TryParse(MinPowerTextBox.Text, out minValue) &&
                        int.TryParse(MaxPowerTextBox.Text, out maxValue))
                    {
                        if (minValue > maxValue)
                        {
                            MaxPowerTextBox.Text = minValue.ToString();
                            MinPowerTextBox.Text = maxValue.ToString();
                        }
                    }
                }
            }
        }

        // Обработчик изменения текста в поле максимальной мощности
        private void MaxPowerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MaxPowerTextBox.Text))
            {
                if (!string.IsNullOrEmpty(MinPowerTextBox.Text))
                {
                    int minValue, maxValue;
                    if (int.TryParse(MinPowerTextBox.Text, out minValue) &&
                        int.TryParse(MaxPowerTextBox.Text, out maxValue))
                    {
                        if (maxValue < minValue)
                        {
                            MinPowerTextBox.Text = maxValue.ToString();
                            MaxPowerTextBox.Text = minValue.ToString();
                        }
                    }
                }
            }
        }

        // Обработчик для кнопки "В избранное"
        private void FavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                if (button.Content.ToString() == "В избранное")
                {
                    button.Content = "✓ В избранном";
                    button.Foreground = System.Windows.Media.Brushes.White;
                    button.Background = System.Windows.Media.Brushes.Green;
                    button.BorderBrush = System.Windows.Media.Brushes.Green;
                    MessageBox.Show("Автомобиль добавлен в избранное", "Избранное",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    button.Content = "В избранное";
                    button.Foreground = System.Windows.Media.Brushes.Gray;
                    button.Background = System.Windows.Media.Brushes.Transparent;
                    button.BorderBrush = System.Windows.Media.Brushes.Gray;
                    MessageBox.Show("Автомобиль удален из избранного", "Избранное",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        // Обработчик для кнопки "Расширять график"
        private void ExpandScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Открытие расширенного графика платежей", "График платежей",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Автоматический поиск при изменении полей фильтров
        private void PerformSearch()
        {
            string brand = BrandTextBox.Text;
            string minPower = MinPowerTextBox.Text;
            string maxPower = MaxPowerTextBox.Text;

            // Здесь можно добавить логику фильтрации автомобилей
            // на основе введенных значений
        }
    }
}