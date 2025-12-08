using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PRAKTICA
{
    public partial class Window5 : Window
    {
        private ObservableCollection<Car> _cars;
        private List<Car> _allCars;

        public Window5()
        {
            InitializeComponent();
            Loaded += Window5_Loaded;

            // Добавляем обработчик события TextChanged для поиска
            SearchTextBox.TextChanged += SearchTextBox_TextChanged;
        }

        private void Window5_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCarsData();
            CalculateTotalValue();
        }

        private void LoadCarsData()
        {
            // Загрузка тестовых данных об автомобилях
            _allCars = new List<Car>
            {
                new Car
                {
                    Id = 1,
                    Name = "Toyota Camry",
                    Price = 2500000,
                    Quantity = 3,
                    Power = 249,
                    Transmission = "Автомат",
                    Color = "Черный",
                    Year = 2023,
                    SteeringWheel = "Левый",
                    Mileage = 15000,
                    Keys = "2 комплекта",
                    City = "Москва",
                    EngineVolume = 2.5
                },
                new Car
                {
                    Id = 2,
                    Name = "BMW X5",
                    Price = 6500000,
                    Quantity = 2,
                    Power = 340,
                    Transmission = "Автомат",
                    Color = "Белый",
                    Year = 2022,
                    SteeringWheel = "Левый",
                    Mileage = 25000,
                    Keys = "1 комплект",
                    City = "Санкт-Петербург",
                    EngineVolume = 3.0
                },
                new Car
                {
                    Id = 3,
                    Name = "Mercedes-Benz E-Class",
                    Price = 5200000,
                    Quantity = 1,
                    Power = 299,
                    Transmission = "Автомат",
                    Color = "Серый",
                    Year = 2023,
                    SteeringWheel = "Левый",
                    Mileage = 10000,
                    Keys = "2 комплекта",
                    City = "Казань",
                    EngineVolume = 2.0
                },
                new Car
                {
                    Id = 4,
                    Name = "Audi Q7",
                    Price = 7200000,
                    Quantity = 2,
                    Power = 340,
                    Transmission = "Автомат",
                    Color = "Синий",
                    Year = 2021,
                    SteeringWheel = "Левый",
                    Mileage = 35000,
                    Keys = "1 комплект",
                    City = "Екатеринбург",
                    EngineVolume = 3.0
                },
                new Car
                {
                    Id = 5,
                    Name = "Lexus RX",
                    Price = 5800000,
                    Quantity = 4,
                    Power = 299,
                    Transmission = "Автомат",
                    Color = "Красный",
                    Year = 2022,
                    SteeringWheel = "Левый",
                    Mileage = 18000,
                    Keys = "2 комплекта",
                    City = "Новосибирск",
                    EngineVolume = 2.5
                },
                new Car
                {
                    Id = 6,
                    Name = "Hyundai Solaris",
                    Price = 1500000,
                    Quantity = 8,
                    Power = 123,
                    Transmission = "Механика",
                    Color = "Серебристый",
                    Year = 2023,
                    SteeringWheel = "Левый",
                    Mileage = 5000,
                    Keys = "2 комплекта",
                    City = "Москва",
                    EngineVolume = 1.6
                },
                new Car
                {
                    Id = 7,
                    Name = "Kia Rio",
                    Price = 1650000,
                    Quantity = 6,
                    Power = 123,
                    Transmission = "Автомат",
                    Color = "Белый",
                    Year = 2023,
                    SteeringWheel = "Левый",
                    Mileage = 8000,
                    Keys = "2 комплекта",
                    City = "Москва",
                    EngineVolume = 1.6
                },
                new Car
                {
                    Id = 8,
                    Name = "Volkswagen Tiguan",
                    Price = 3200000,
                    Quantity = 3,
                    Power = 180,
                    Transmission = "Автомат",
                    Color = "Черный",
                    Year = 2022,
                    SteeringWheel = "Левый",
                    Mileage = 22000,
                    Keys = "1 комплект",
                    City = "Санкт-Петербург",
                    EngineVolume = 2.0
                },
                new Car
                {
                    Id = 9,
                    Name = "Skoda Octavia",
                    Price = 2200000,
                    Quantity = 5,
                    Power = 150,
                    Transmission = "Автомат",
                    Color = "Серый",
                    Year = 2023,
                    SteeringWheel = "Левый",
                    Mileage = 12000,
                    Keys = "2 комплекта",
                    City = "Казань",
                    EngineVolume = 1.4
                },
                new Car
                {
                    Id = 10,
                    Name = "Nissan Qashqai",
                    Price = 2800000,
                    Quantity = 4,
                    Power = 150,
                    Transmission = "Вариатор",
                    Color = "Синий",
                    Year = 2022,
                    SteeringWheel = "Левый",
                    Mileage = 19000,
                    Keys = "2 комплекта",
                    City = "Екатеринбург",
                    EngineVolume = 2.0
                }
            };

            _cars = new ObservableCollection<Car>(_allCars);
            CarsDataGrid.ItemsSource = _cars;
        }

        private void CalculateTotalValue()
        {
            decimal totalValue = _cars.Sum(car => car.Price * car.Quantity);
            TotalValueText.Text = $"Общая стоимость: {totalValue:N0} ₽";
        }

        // Обработчик для изменения текста в поиске
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterCars();
        }

        private void FilterCars()
        {
            string searchText = SearchTextBox.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                _cars = new ObservableCollection<Car>(_allCars);
            }
            else
            {
                var filtered = _allCars.Where(car =>
                    car.Name.ToLower().Contains(searchText) ||
                    car.Color.ToLower().Contains(searchText) ||
                    car.City.ToLower().Contains(searchText) ||
                    car.Transmission.ToLower().Contains(searchText) ||
                    car.SteeringWheel.ToLower().Contains(searchText) ||
                    car.Keys.ToLower().Contains(searchText) ||
                    car.Year.ToString().Contains(searchText)
                ).ToList();

                _cars = new ObservableCollection<Car>(filtered);
            }

            CarsDataGrid.ItemsSource = _cars;
            CalculateTotalValue();
        }

        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция добавления товара в разработке", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EditCar_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int carId)
            {
                var car = _allCars.FirstOrDefault(c => c.Id == carId);
                if (car != null)
                {
                    MessageBox.Show($"Редактирование автомобиля: {car.Name}\nЦена: {car.Price:N0} ₽\nКоличество: {car.Quantity}",
                        "Редактирование", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void DeleteCar_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int carId)
            {
                var car = _allCars.FirstOrDefault(c => c.Id == carId);
                if (car != null)
                {
                    var result = MessageBox.Show(
                        $"Вы уверены, что хотите удалить автомобиль '{car.Name}'?\nЦена: {car.Price:N0} ₽\nКоличество: {car.Quantity}\n\nЭто действие нельзя отменить.",
                        "Подтверждение удаления",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        _allCars.Remove(car);
                        FilterCars();

                        MessageBox.Show($"Автомобиль '{car.Name}' успешно удален!", "Успех",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        private void RefreshData_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Clear();
            LoadCarsData();
            CalculateTotalValue();

            MessageBox.Show("Данные обновлены", "Обновление",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            FilterCars();
        }

        // ИЗМЕНЕННЫЙ МЕТОД ВЫХОДА - ПРОСТО ЗАКРЫВАЕМ ОКНО
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Вы уверены, что хотите выйти из админ-панели?",
                "Выход",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // ПРОСТО ЗАКРЫВАЕМ ОКНО
                this.Close();
            }
        }

        private void CarsDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }

    // Класс для хранения данных об автомобиле
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Power { get; set; }
        public string Transmission { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public string SteeringWheel { get; set; }
        public int Mileage { get; set; }
        public string Keys { get; set; }
        public string City { get; set; }
        public double EngineVolume { get; set; }
    }
}