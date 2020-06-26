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
        /// Сервис для открытия окна
        /// </summary>
        private readonly IWindowService _windowService = new WindowService();

        #endregion

        #region Properties

        /// <summary>
        /// Команда на осуществление поиска
        /// </summary>
        public RelayCommand ApplyCommand { get; private set; }

        /// <summary>
        /// Команда на очистку полей окна от введенных данных
        /// </summary>
        public RelayCommand ClearDataCommand { get; private set; }

        /// <summary>
        /// Выбран поиск по размеру скидки
        /// </summary>
        public bool IsDiscountEnabled { get; set; }

        /// <summary>
        /// Выбран поиск по исходной цене
        /// </summary>
        public bool IsInitialCostEnabled { get; set; }

        /// <summary>
        /// Выбран поиск по типу скидки
        /// </summary>
        public bool IsSaleSystemEnabled { get; set; }

        /// <summary>
        /// Команда на загрузку данных
        /// </summary>
        public RelayCommand LoadSalesCommand { get; private set; }

        /// <summary>
        /// Команда на открытие окна добавления скидки
        /// </summary>
        public RelayCommand OpenAddingSaleWindowCommand { get; private set; }

        /// <summary>
        /// Возможные системы скидок для поиска
        /// </summary>
        public List<ISales> PossibleSalesSystems { get; private set; } =
            new List<ISales>() { new CertificateSale(), new PercentSale() };

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
        /// Скидка для заполнения поисковых полей
        /// </summary>
        public ISales SaleToFill { get; private set; }

        /// <summary>
        /// Команда на сохранение данных
        /// </summary>
        public RelayCommand SaveSalesCommand { get; private set; }
        
        /// <summary>
        /// Список скидок для поиска
        /// </summary>
        public IList<ISales> SearchedSales { get; private set; } =
            new ObservableCollection<ISales>();

        /// <summary>
        /// Выбранная в списке скидка
        /// </summary>
        public ISales SelectedSale { get; set; }

        /// <summary>
        /// Выбранная в списке поиска скидка
        /// </summary>
        public ISales SelectedSearchedSale { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            SaleToFill = new CertificateSale();
            SelectedSearchedSale = PossibleSalesSystems[0];

#if DEBUG
            Sales.Add(new CertificateSale(150, 50));
            Sales.Add(new CertificateSale(10130, 3699));
            Sales.Add(new PercentSale(7999, 33));
            Sales.Add(new PercentSale(785000, 13));
#endif

            ApplyCommand = new RelayCommand(Apply);
            ClearDataCommand = new RelayCommand(ClearData);
            OpenAddingSaleWindowCommand = new RelayCommand(
                OpenAddingSaleWindow);
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
        /// Осуществить поиск по заданным параметрам
        /// </summary>
        private void Apply()
        {
            SearchedSales.Clear();

            if (IsSaleSystemEnabled)
            {
                if (IsInitialCostEnabled)
                {
                    if (IsDiscountEnabled)
                    {
                        foreach (ISales sale in Sales)
                        {
                            if (sale.GetType() == 
                                SelectedSearchedSale.GetType() &&
                                sale.InitialCost == SaleToFill.InitialCost &&
                                sale.Discount == SaleToFill.Discount)
                            {
                                SearchedSales.Add(sale);
                            }
                        }
                    }
                    else
                    {
                        foreach (ISales sale in Sales)
                        {
                            if (sale.GetType() == 
                                SelectedSearchedSale.GetType() &&
                                sale.InitialCost == SaleToFill.InitialCost)
                            {
                                SearchedSales.Add(sale);
                            }
                        }
                    }
                }
                else
                {
                    if (IsDiscountEnabled)
                    {
                        foreach (ISales sale in Sales)
                        {
                            if (sale.GetType() == 
                                SelectedSearchedSale.GetType() &&
                                sale.Discount == SaleToFill.Discount)
                            {
                                SearchedSales.Add(sale);
                            }
                        }
                    }
                    else
                    {
                        foreach (ISales sale in Sales)
                        {
                            if (sale.GetType() == 
                                SelectedSearchedSale.GetType())
                            {
                                SearchedSales.Add(sale);
                            }
                        }
                    }
                }
            }
            else
            {
                if (IsInitialCostEnabled)
                {
                    if (IsDiscountEnabled)
                    {
                        foreach (ISales sale in Sales)
                        {
                            if (sale.InitialCost == SaleToFill.InitialCost &&
                                sale.Discount == SaleToFill.Discount)
                            {
                                SearchedSales.Add(sale);
                            }
                        }
                    }
                    else
                    {
                        foreach (ISales sale in Sales)
                        {
                            if (sale.InitialCost == SaleToFill.InitialCost)
                            {
                                SearchedSales.Add(sale);
                            }
                        }
                    }
                }
                else
                {
                    if (IsDiscountEnabled)
                    {
                        foreach (ISales sale in Sales)
                        {
                            if (sale.Discount == SaleToFill.Discount)
                            {
                                SearchedSales.Add(sale);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Очистить поля поиска
        /// </summary>
        private void ClearData()
        {
            SaleToFill = new CertificateSale();
            RaisePropertyChanged(nameof(SaleToFill));
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

            if (dialog.FileName == String.Empty)
            {
                return;
            }

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
            catch (System.Text.DecoderFallbackException)
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

            if (dialog.FileName == String.Empty)
            {
                return;
            }

            _pathToSaveFile = dialog.FileName;
            Serializer.SerializeData<ISales>(
                new List<ISales>(Sales), _pathToSaveFile);
        }

        #endregion
    }
}