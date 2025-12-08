using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CapitalLeasing
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            // Подписываемся на события
            ContinueButton.Click += ContinueButton_Click;

            // Автоматически заполняем некоторые поля для тестирования
            LoadSampleData();

            // Устанавливаем фокус на первое поле
            Loaded += (s, e) => LastNameTextBox.Focus();
        }

        private void LoadSampleData()
        {
            // Для тестирования - можно удалить в боевой версии
            LastNameTextBox.Text = "Иванов";
            FirstNameTextBox.Text = "Иван";
            MiddleNameTextBox.Text = "Иванович";
            LoginTextBox.Text = "ivanov_ii";
            PasswordBox.Password = "Qwerty123!";
            PhoneTextBox.Text = "+7 (999) 123-45-67";
            EmailTextBox.Text = "ivanov@example.com";
            CaptchaTextBox.Text = "CAPTCHA";
            AgreementCheckBox.IsChecked = true;
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем все обязательные поля
            if (!ValidateForm())
            {
                return;
            }

            try
            {
                // Собираем данные пользователя
                var userData = new UserRegistrationData
                {
                    LastName = LastNameTextBox.Text.Trim(),
                    FirstName = FirstNameTextBox.Text.Trim(),
                    MiddleName = MiddleNameTextBox.Text.Trim(),
                    Login = LoginTextBox.Text.Trim(),
                    Password = PasswordBox.Password,
                    Phone = PhoneTextBox.Text.Trim(),
                    Email = EmailTextBox.Text.Trim()
                };

                // Здесь будет логика сохранения данных
                ProcessRegistration(userData);

                // Показываем сообщение об успехе
                MessageBox.Show(
                    "Регистрация успешно завершена!\n\n" +
                    $"Логин: {userData.Login}\n" +
                    $"Имя: {userData.FirstName} {userData.LastName}\n" +
                    $"Email: {userData.Email}",
                    "Успешная регистрация",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                // ВОТ ИЗМЕНЕНИЕ - ПЕРЕНОСИМ НА ГЛАВНУЮ СТРАНИЦУ (Window2)
                OpenMainPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка при регистрации: {ex.Message}",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        // НОВЫЙ МЕТОД - ОТКРЫТЬ ГЛАВНУЮ СТРАНИЦУ (Window2)
        private void OpenMainPage()
        {
            try
            {
                // Создаем окно главной страницы (Window2)
                CatalogWindow mainPage = new CatalogWindow();

                // Можно передать данные пользователя, если нужно
                // mainPage.CurrentUserLogin = LoginTextBox.Text.Trim();
                // mainPage.CurrentUserName = $"{FirstNameTextBox.Text.Trim()} {LastNameTextBox.Text.Trim()}";

                // Показываем главную страницу
                mainPage.Show();

                // Закрываем текущее окно регистрации
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Не удалось открыть главную страницу: {ex.Message}\n\n" +
                    "Убедитесь что файл Window2.xaml существует в проекте.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private bool ValidateForm()
        {
            // Сбрасываем подсветку ошибок
            ResetValidation();

            bool isValid = true;

            // Проверка фамилии
            if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
            {
                HighlightError(LastNameTextBox, "Введите фамилию");
                isValid = false;
            }

            // Проверка имени
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
            {
                HighlightError(FirstNameTextBox, "Введите имя");
                isValid = false;
            }

            // Проверка логина
            if (string.IsNullOrWhiteSpace(LoginTextBox.Text))
            {
                HighlightError(LoginTextBox, "Введите логин");
                isValid = false;
            }
            else if (LoginTextBox.Text.Length < 3)
            {
                HighlightError(LoginTextBox, "Логин должен содержать минимум 3 символа");
                isValid = false;
            }

            // Проверка пароля
            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                HighlightError(PasswordBox, "Введите пароль");
                isValid = false;
            }
            else if (PasswordBox.Password.Length < 6)
            {
                HighlightError(PasswordBox, "Пароль должен содержать минимум 6 символов");
                isValid = false;
            }
            else if (!IsPasswordStrong(PasswordBox.Password))
            {
                HighlightError(PasswordBox, "Пароль должен содержать буквы и цифры");
                isValid = false;
            }

            // Проверка телефона
            if (string.IsNullOrWhiteSpace(PhoneTextBox.Text))
            {
                HighlightError(PhoneTextBox, "Введите номер телефона");
                isValid = false;
            }
            else if (!IsValidPhone(PhoneTextBox.Text))
            {
                HighlightError(PhoneTextBox, "Введите корректный номер телефона");
                isValid = false;
            }

            // Проверка email
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                HighlightError(EmailTextBox, "Введите email");
                isValid = false;
            }
            else if (!IsValidEmail(EmailTextBox.Text))
            {
                HighlightError(EmailTextBox, "Введите корректный email");
                isValid = false;
            }

            // Проверка капчи
            if (string.IsNullOrWhiteSpace(CaptchaTextBox.Text))
            {
                HighlightError(CaptchaTextBox, "Введите код с картинки");
                isValid = false;
            }
            else if (CaptchaTextBox.Text.ToUpper() != "CAPTCHA")
            {
                HighlightError(CaptchaTextBox, "Неверный код с картинки");
                isValid = false;
            }

            // Проверка согласия
            if (AgreementCheckBox.IsChecked != true)
            {
                MessageBox.Show(
                    "Для продолжения регистрации необходимо согласиться на обработку персональных данных",
                    "Требуется согласие",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                isValid = false;
            }

            return isValid;
        }

        private void HighlightError(Control control, string errorMessage)
        {
            // Подсвечиваем поле с ошибкой
            control.BorderBrush = Brushes.Red;
            control.BorderThickness = new Thickness(2);
            control.ToolTip = errorMessage;

            // Прокручиваем к полю с ошибкой
            control.Focus();
        }

        private void ResetValidation()
        {
            // Сбрасываем подсветку для всех полей
            var controls = new Control[]
            {
                LastNameTextBox,
                FirstNameTextBox,
                MiddleNameTextBox,
                LoginTextBox,
                PasswordBox,
                PhoneTextBox,
                EmailTextBox,
                CaptchaTextBox
            };

            foreach (var control in controls)
            {
                control.BorderBrush = new SolidColorBrush(Color.FromRgb(206, 212, 218));
                control.BorderThickness = new Thickness(1);
                control.ToolTip = null;
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhone(string phone)
        {
            // Простая проверка номера телефона
            var cleanedPhone = Regex.Replace(phone, @"[^\d+]", "");
            return cleanedPhone.Length >= 10;
        }

        private bool IsPasswordStrong(string password)
        {
            // Проверяем, что пароль содержит хотя бы одну букву и одну цифру
            return Regex.IsMatch(password, "[a-zA-Z]") && Regex.IsMatch(password, @"\d");
        }

        private void ProcessRegistration(UserRegistrationData userData)
        {
            // Здесь должна быть реальная логика регистрации
            // Для примера просто логируем
            Console.WriteLine($"Регистрация пользователя: {userData.LastName} {userData.FirstName}");
            Console.WriteLine($"Логин: {userData.Login}");
            Console.WriteLine($"Email: {userData.Email}");
            Console.WriteLine($"Телефон: {userData.Phone}");

            // Имитация задержки при обработке
            System.Threading.Thread.Sleep(1000);
        }

        // Вспомогательный класс для хранения данных
        public class UserRegistrationData
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
        }

        // Обработчики событий для валидации в реальном времени
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.BorderBrush = new SolidColorBrush(Color.FromRgb(206, 212, 218));
                textBox.BorderThickness = new Thickness(1);
                textBox.ToolTip = null;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                passwordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(206, 212, 218));
                passwordBox.BorderThickness = new Thickness(1);
                passwordBox.ToolTip = null;
            }
        }
    }
}