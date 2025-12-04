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
            RegisterButton.Click += RegisterButton_Click;  // Открывает Window1 (регистрация)
            TelegramButton.Click += TelegramButton_Click;
            HelpButton.Click += HelpButton_Click;

            // Обработчики для текстовых полей (опционально)
            LoginTextBox.KeyDown += TextBox_KeyDown;
            PasswordBox.KeyDown += PasswordBox_KeyDown;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = LoginTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля", "Ошибка входа",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Здесь ваша логика авторизации
            // ...
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
                // Открываем окно регистрации Window1
                Window4 registrationWindow = new Window4();

                // Скрываем текущее окно входа
                this.Hide();

                // Показываем окно регистрации
                registrationWindow.Show();

                // Когда окно регистрации закроется, покажем главное окно снова
                registrationWindow.Closed += (s, args) =>
                {
                    this.Show(); // Показываем окно входа снова
                    this.Activate(); // Активируем его
                    ClearFields(); // Очищаем поля (опционально)
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии окна регистрации: {ex.Message}",
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
            MessageBox.Show("Телефон поддержки: 8 322 228-14-88\nEmail: support@capitalleasing.ru",
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

        private void RegisterButton_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}