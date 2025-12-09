using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CapitalLeasing
{
    public partial class CatalogWindow : Window
    {
        public CatalogWindow()
        {
            InitializeComponent();
            AttachEventHandlers();
        }

        private void AttachEventHandlers()
        {
            // Обработчики для кнопок навигации
            FindButtonClick("Каталог авто", CatalogButton_Click);
            FindButtonClick("Клиентский сервис", ServiceButton_Click);
            FindButtonClick("Личный кабинет", AccountButton_Click);
            FindButtonClick("Контакты", ContactsButton_Click);

            // Кнопка поиска
            if (SearchButton != null)
                SearchButton.Click += SearchButton_Click;

            // Обработчики для переключателей
            if (NewRadio != null)
                NewRadio.Checked += CarTypeRadio_Checked;
            if (UsedRadio != null)
                UsedRadio.Checked += CarTypeRadio_Checked;

            // Обработчики для выпадающих списков
            if (BrandCombo != null)
                BrandCombo.SelectionChanged += FilterCombo_SelectionChanged;
            if (ModelCombo != null)
                ModelCombo.SelectionChanged += FilterCombo_SelectionChanged;
            if (PriceCombo != null)
                PriceCombo.SelectionChanged += FilterCombo_SelectionChanged;

            // Двойной клик по иконке профиля
            var profileIcon = FindProfileIcon();
            if (profileIcon != null)
                profileIcon.MouseDown += ProfileIcon_MouseDown;
        }

        private void FindButtonClick(string content, RoutedEventHandler handler)
        {
            foreach (var child in LogicalTreeHelper.GetChildren(this))
            {
                if (child is Button button && button.Content?.ToString() == content)
                {
                    button.Click += handler;
                    break;
                }
            }
        }

        private Border FindProfileIcon()
        {
            foreach (var child in LogicalTreeHelper.GetChildren(this))
            {
                if (child is Border border && border.Background?.ToString() == "#007BFF")
                {
                    return border;
                }
            }
            return null;
        }

        // КНОПКА "КАТАЛОГ АВТО"
        private void CatalogButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы уже находитесь в каталоге автомобилей", "Каталог",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // КНОПКА "КЛИЕНТСКИЙ СЕРВИС"
        private void ServiceButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Клиентский сервис\n\n" +
                          "• Консультации по лизингу\n" +
                          "• Подбор автомобиля\n" +
                          "• Оформление документов\n" +
                          "• Техническая поддержка\n\n" +
                          "Телефон: 8.322.228-14-88",
                          "Клиентский сервис",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // КНОПКА "ЛИЧНЫЙ КАБИНЕТ"
        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция личного кабинета в разработке\n\n" +
                          "В будущем здесь будет:\n" +
                          "• История заявок\n" +
                          "• Мои автомобили\n" +
                          "• Настройки профиля\n" +
                          "• Уведомления",
                          "Личный кабинет",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // КНОПКА "КОНТАКТЫ"
        private void ContactsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Контакты Capital Leasing\n\n" +
                          "📍 Москва, ул. Автомобильная, 123\n" +
                          "📞 8.322.228-14-88\n" +
                          "📧 info@capitalleasing.ru\n" +
                          "🕒 Пн-Пт: 9:00-20:00, Сб-Вс: 10:00-18:00\n\n" +
                          "Telegram: @capitalleasing_support",
                          "Контакты",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // КНОПКА "НАЙТИ!"
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string brand = "Любая";
            string price = "Любая";
            string carType = "Новые";

            if (BrandCombo != null && BrandCombo.SelectedItem is ComboBoxItem brandItem)
                brand = brandItem.Content?.ToString() ?? "Любая";

            if (PriceCombo != null && PriceCombo.SelectedItem is ComboBoxItem priceItem)
                price = priceItem.Content?.ToString() ?? "Любая";

            if (NewRadio != null && NewRadio.IsChecked == true)
                carType = "Новые";
            else if (UsedRadio != null && UsedRadio.IsChecked == true)
                carType = "С пробегом";

            MessageBox.Show($"Поиск автомобилей:\n\n" +
                          $"Тип: {carType}\n" +
                          $"Марка: {brand}\n" +
                          $"Стоимость: {price}\n\n" +
                          $"Результаты поиска будут отображены ниже.",
                          "Поиск",
                          MessageBoxButton.OK, MessageBoxImage.Information);

            // Здесь можно добавить логику реального поиска
            // LoadSearchResults(brand, price, carType);
        }

        // ПЕРЕКЛЮЧАТЕЛИ "НОВЫЕ/С ПРОБЕГОМ"
        private void CarTypeRadio_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radio)
            {
                // Обновляем модели в зависимости от выбранного типа
                UpdateModelsByCarType(radio.Content.ToString());
            }
        }

        private void UpdateModelsByCarType(string carType)
        {
            if (ModelCombo == null) return;

            ModelCombo.Items.Clear();

            // Добавляем "Любая" модель
            var anyModel = new ComboBoxItem { Content = "Любая", IsSelected = true };
            ModelCombo.Items.Add(anyModel);

            // Добавляем модели в зависимости от типа авто
            if (carType == "Новые")
            {
                AddModelItems(new[] { "Седан", "Кроссовер", "Внедорожник", "Хэтчбек", "Купе" });
            }
            else // С пробегом
            {
                AddModelItems(new[] { "Седан", "Универсал", "Минивэн", "Пикап", "Кабриолет" });
            }
        }

        private void AddModelItems(string[] models)
        {
            foreach (var model in models)
            {
                ModelCombo.Items.Add(new ComboBoxItem { Content = model });
            }
        }

        // ИЗМЕНЕНИЕ ФИЛЬТРОВ
        private void FilterCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Можно добавить логику динамического обновления фильтров
            // Например, при выборе марки обновить список моделей
        }

        // КЛИК ПО ИКОНКЕ ПРОФИЛЯ
        private void ProfileIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) // Двойной клик
            {
                ShowProfileMenu();
            }
        }

        private void ShowProfileMenu()
        {
            MessageBox.Show("Профиль пользователя\n\n" +
                          "• Настройки аккаунта\n" +
                          "• Мои заявки\n" +
                          "• Избранное\n" +
                          "• История просмотров\n" +
                          "• Выйти\n\n" +
                          "Функция в разработке",
                          "Мой профиль",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // МЕТОД ДЛЯ ЗАГРУЗКИ РЕЗУЛЬТАТОВ ПОИСКА
        private void LoadSearchResults(string brand, string price, string carType)
        {
            // Здесь должна быть логика загрузки автомобилей из базы данных
            // Пример:
            /*
            var cars = GetCarsFromDatabase(brand, price, carType);
            
            // Очищаем контейнер
            ResultsContainer.Children.Clear();
            
            foreach (var car in cars)
            {
                var carCard = CreateCarCard(car);
                ResultsContainer.Children.Add(carCard);
            }
            */
        }

        // ДОПОЛНИТЕЛЬНЫЕ МЕТОДЫ

        // БЫСТРЫЙ ПОИСК ПО МАРКЕ
        public void QuickSearchByBrand(string brand)
        {
            if (BrandCombo != null)
            {
                foreach (ComboBoxItem item in BrandCombo.Items)
                {
                    if (item.Content?.ToString() == brand)
                    {
                        BrandCombo.SelectedItem = item;
                        break;
                    }
                }
                SearchButton_Click(null, null);
            }
        }

        // БЫСТРЫЙ ПОИСК ПО ЦЕНЕ
        public void QuickSearchByPrice(string priceRange)
        {
            if (PriceCombo != null)
            {
                foreach (ComboBoxItem item in PriceCombo.Items)
                {
                    if (item.Content?.ToString() == priceRange)
                    {
                        PriceCombo.SelectedItem = item;
                        break;
                    }
                }
                SearchButton_Click(null, null);
            }
        }

        // ВОЗВРАТ НА ГЛАВНУЮ
        public void NavigateToHome()
        {
            var result = MessageBox.Show("Вернуться на главную страницу?", "Выход",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Если есть MainWindow
                // MainWindow main = new MainWindow();
                // main.Show();
                this.Close();
            }
        }

        // КЛАВИША ESC ДЛЯ ВЫХОДА
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Key.Escape)
            {
                NavigateToHome();
            }
        }
    }
}