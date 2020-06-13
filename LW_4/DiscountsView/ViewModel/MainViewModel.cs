using DiscountCalculatorModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SerializationServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using WPFWindowServices;

namespace DiscountsView.ViewModel
{
    /// <summary>
    /// Модель представления главного окна программы
    /// </summary>
    [Serializable]
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// Модель представления окна добавления объекта
        /// </summary>
        private readonly AddingObjectViewModel _addingObjectViewModel = 
            new AddingObjectViewModel();

        /// <summary>
        /// Путь до папки сохранения файла
        /// </summary>
        private string _pathToSaveFile;

        /// <summary>
        /// Модель представления окна поиска
        /// </summary>
        private readonly SearchViewModel _searchViewModel =
            new SearchViewModel();

        /// <summary>
        /// Сервис для открытия окна
        /// </summary>
        private readonly IWindowService _windowService = new WindowService();

        #endregion

        #region Properties

        /// <summary>
        /// Команда на загрузку данных
        /// </summary>
        public RelayCommand LoadSalesCommand { get; private set; }

        /// <summary>
        /// Команда на открытие окна добавления скидки
        /// </summary>
        public RelayCommand OpenAddingSaleWindowCommand { get; private set; }

        /// <summary>
        /// Команда на открытие окна поиска скидки
        /// </summary>
        public RelayCommand OpenSearchWindowCommand { get; private set; }

        /// <summary>
        /// Команда на удаление выбранной строки со скидкой
        /// </summary>
        public RelayCommand<IList> RemoveSaleCommand { get; private set; }

        /// <summary>
        /// Список систем скидок
        /// </summary>
        public IList<ISales> Sales { get; private set; } =
            new ObservableCollection<ISales>();

        /// <summary>
        /// Команда на сохранение данных
        /// </summary>
        public RelayCommand SaveSalesCommand { get; private set; }

        /// <summary>
        /// Выбранная в списке скидка
        /// </summary>
        public ISales SelectedSale { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
#if DEBUG
            Sales.Add(new CertificateSale(150, 50));
            Sales.Add(new CertificateSale(10130, 3699));
            Sales.Add(new PercentSale(7999, 33));
            Sales.Add(new PercentSale(785000, 13));
#endif

            OpenAddingSaleWindowCommand = new RelayCommand(
                OpenAddingSaleWindow);
            OpenSearchWindowCommand = new RelayCommand(OpenSearchWindow);
            RemoveSaleCommand = new RelayCommand<IList>(RemoveSale);
            SaveSalesCommand = new RelayCommand(SaveSales);
            LoadSalesCommand = new RelayCommand(LoadSales);

            // Регистрация сообщений о добавлении новой скидки
            Messenger.Default.Register<ISales>(
                this, AddSaleIntoList);
        }

        #region Methods

        /// <summary>
        /// Добавление в список новой скидки
        /// </summary>
        /// <param name="sale">Сообщение от VM окна добавления скидки</param>
        private void AddSaleIntoList(ISales sale)
        {
            Sales.Add(sale);
        }

        /// <summary>
        /// Загрузить данные из файла
        /// </summary>
        private void LoadSales()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "svy files (.svy)|*.svy";
            dialog.Title = "Opening list";
            dialog.ShowDialog();
            _pathToSaveFile = dialog.FileName;

            try
            {
                Sales = new ObservableCollection<ISales>(
                    Serializer.DeserializeData<ISales>(_pathToSaveFile));
                RaisePropertyChanged(nameof(Sales));
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Файла не существует!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Директории не существует!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (System.Runtime.Serialization.SerializationException)
            {
                MessageBox.Show("Файл поврежден!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        /// <summary>
        /// Открытие окна "Adding new sale"
        /// </summary>
        private void OpenAddingSaleWindow()
        {
            _windowService.ShowWindow(
                _addingObjectViewModel, "Adding new sale");
        }

        /// <summary>
        /// Открытие окна "Search sales"
        /// </summary>
        private void OpenSearchWindow()
        {
            Messenger.Default.Send(Sales);
            _windowService.ShowWindow(
                _searchViewModel, "Search sales");
        }

        /// <summary>
        /// Удаление скидки
        /// </summary>
        /// <param name="items">Выбранные скидки</param>
        private void RemoveSale(object items)
        {
            var selection = items as IList;
            var count = selection.Count;
            var counter = 0;

            while (counter < count)
            {
                Sales.Remove(selection[0] as ISales);
                counter++;
            }
            
            RaisePropertyChanged(nameof(Sales));
            //if (SelectedSale is object)
            //{
                //Sales.Remove(SelectedSale);
            //}
        }

        /// <summary>
        /// Сохранить данные в файл
        /// </summary>
        private void SaveSales()
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = "svy files (.svy)|*.svy";
            dialog.Title = "Saving list";
            dialog.ShowDialog();
            _pathToSaveFile = dialog.FileName;

            Serializer.SerializeData<ISales>(
                new List<ISales>(Sales), _pathToSaveFile);
        }

        #endregion
    }
}