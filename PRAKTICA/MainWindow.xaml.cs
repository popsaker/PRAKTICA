using Example;
using PRAKTICA;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace CapitalLeasing
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AttachEventHandlers();
        }

        private void AttachEventHandlers()
        {
            // Обработчики для кнопок
            LoginButton.Click += LoginButton_Click;
            ForgotPasswordButton.Click += ForgotPasswordButton_Click;
            RegisterButton.Click += RegisterButton_Click;
            TelegramButton.Click += TelegramButton_Click;
            HelpButton.Click += HelpButton_Click;

            // Обработчики для Enter
            LoginTextBox.KeyDown += TextBox_KeyDown;
            PasswordBox.KeyDown += PasswordBox_KeyDown;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем логин из текстового поля и удаляем лишние пробелы
            string login = LoginTextBox.Text.Trim();
            // Получаем пароль из поля для пароля (PasswordBox скрывает ввод)
            string password = PasswordBox.Password;

<<<<<<< HEAD
            // Валидация ввода: проверяем, что оба поля заполнены
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
=======
            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
>>>>>>> ada5c2d99bd0e8fd81a7a3b4a0fa21e8729fadf5
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }

<<<<<<< HEAD
            // Вызываем метод аутентификации из сервиса базы данных
            // DbService.AuthenticateUser проверяет логин/пароль в базе данных
            var user = DbService.AuthenticateUser(login, password);

            // Проверяем результат аутентификации
            if (user != null)
            {
                // Если пользователь найден (аутентификация успешна):

                // Открываем окно, соответствующее роли пользователя
                Window3 window3 = new Window3();
                window3.Show();
                // скрываем, но не уничтожаем
                // Окно невидимо, но продолжает существовать в памяти
                // Можно вернуться к нему позже (например, при разлогине)
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }

            // Здесь ваша логика авторизации
=======
            // ПРОВЕРКА НА АДМИНА
            if (username == "Admin" && password == "123")
            {
                // ОТКРЫВАЕМ АДМИН ПАНЕЛЬ (Window5)
                OpenAdminPanel();
            }
            else
            {
                // Обычный вход для пользователей - ОТКРЫВАЕМ WINDOW2
                PerformUserLogin(username, password);
            }
        }

        // МЕТОД ДЛЯ ОТКРЫТИЯ АДМИН ПАНЕЛИ
        private void OpenAdminPanel()
        {
            try
            {
                // Создаем окно админ панели
                Window5 adminPanel = new Window5();

                // Закрываем текущее окно
                this.Close();

                // Открываем админ панель
                adminPanel.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть админ панель: {ex.Message}\n\n" +
                              "Убедитесь что файл Window5.xaml существует в проекте.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        // МЕТОД ДЛЯ ОБЫЧНЫХ ПОЛЬЗОВАТЕЛЕЙ - ОТКРЫВАЕМ WINDOW2
        private void PerformUserLogin(string username, string password)
        {
            // ТЕСТОВЫЕ УЧЕТКИ ДЛЯ ОБЫЧНЫХ ПОЛЬЗОВАТЕЛЕЙ
            if ((username == "user" && password == "123") ||
                (username == "test" && password == "test"))
            {
                // ОТКРЫВАЕМ WINDOW2 (КАБИНЕТ/КАТАЛОГ)
                OpenWindow2(username);
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!\n\n" +
                              "Для теста используйте:\n" +
                              "• Админ: Логин = Admin, Пароль = 123\n" +
                              "• Пользователь: Логин = user, Пароль = 123",
                    "Ошибка входа",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                // Очищаем поле пароля
                PasswordBox.Clear();
                PasswordBox.Focus();
            }
        }

        // НОВЫЙ МЕТОД - ОТКРЫТИЕ WINDOW2 ДЛЯ ПОЛЬЗОВАТЕЛЕЙ
        private void OpenWindow2(string username)
        {
            try
            {
                // Создаем окно Window2
                CatalogWindow userWindow = new CatalogWindow();

                // Можешь передать имя пользователя в Window2 если нужно
                // userWindow.CurrentUserName = username;

                // Закрываем текущее окно
                this.Close();

                // Открываем Window2
                userWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть главную страницу: {ex.Message}\n\n" +
                              "Убедитесь что файл Window2.xaml существует в проекте.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
>>>>>>> ada5c2d99bd0e8fd81a7a3b4a0fa21e8729fadf5
        }

        private void ForgotPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция восстановления пароля в разработке", "Восстановление пароля",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
<<<<<<< HEAD
                // Открываем окно регистрации Window4
                Window4 registrationWindow = new Window4();
=======
                // Открываем окно регистрации Window1
                Window1 registrationWindow = new Window1();
>>>>>>> ada5c2d99bd0e8fd81a7a3b4a0fa21e8729fadf5

                // Закрываем текущее окно входа
                this.Close();

                // Показываем окно регистрации
                registrationWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть окно регистрации: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TelegramButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string telegramUrl = "https://t.me/popsaker";
                Process.Start(new ProcessStartInfo
                {
                    FileName = telegramUrl,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть Telegram: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Телефон поддержки: 8 322 228-14-88\nEmail: support@capitalleasing.ru\n\n" +
                          "Тестовые данные для входа:\n" +
                          "• Админ: Логин = Admin, Пароль = 123 → Админ панель (Window5)\n" +
                          "• Пользователь: Логин = user, Пароль = 123 → Главная страница (Window2)",
                "Помощь и поддержка",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                PasswordBox.Focus();
            }
        }

        private void PasswordBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }

        // Метод для очистки полей
        public void ClearFields()
        {
            LoginTextBox.Text = "";
            PasswordBox.Password = "";
            LoginTextBox.Focus();
        }

        // Для быстрого теста
        public void AutoFillAdmin()
        {
            LoginTextBox.Text = "Admin";
            PasswordBox.Password = "123";
        }

        public void AutoFillUser()
        {
            LoginTextBox.Text = "user";
            PasswordBox.Password = "123";
        }
    }
}