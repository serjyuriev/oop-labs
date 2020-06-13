using DiscountCalculatorModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace DiscountsView.ViewModel
{
    /// <summary>
    /// Модель представления для окна добавления скидки
    /// </summary>
    public class AddingObjectViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Команда на создание новой скидки
        /// </summary>
        public RelayCommand AddCreatedSale { get; private set; }

#if DEBUG
        /// <summary>
        /// Команда на заполнение полей случайными значениями
        /// </summary>
        public RelayCommand CreateRandomDataCommand { get; private set; }
#endif

        /// <summary>
        /// Высота последнего Grid в окне
        /// </summary>
        public GridLength LastGridHeight
        {
#if DEBUG
            get => new GridLength(1, GridUnitType.Star);
#else
                get => new GridLength(0);
#endif
        }

        /// <summary>
        /// Лист с доступными системами скидок
        /// </summary>
        public IList<ISales> Sales { get; } =
            new ObservableCollection<ISales>();

        /// <summary>
        /// Выбранная система скидок
        /// </summary>
        public ISales SelectedSale { get; set; }

        /// <summary>
        /// Высота окна
        /// </summary>
        public double WindowHeight
        {
#if DEBUG
            get => 350;
#else
            get => 300;
#endif
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the AddingObjectViewModel class
        /// </summary>
        public AddingObjectViewModel()
        {
            RefreshList();

            AddCreatedSale = new RelayCommand(SendNewSaleToMainWindow);
#if DEBUG
            CreateRandomDataCommand = new RelayCommand(CreateRandomData);
#endif
        }

        #region Methods

#if DEBUG
        /// <summary>
        /// Заполнение полей случайными значениями
        /// </summary>
        private void CreateRandomData()
        {
            var generatedValues = Randomizer.GetRandomValuesForSales();

            SelectedSale = Sales[(int)(1 - generatedValues[0])];
            RaisePropertyChanged(nameof(SelectedSale));

            SelectedSale = Sales[(int)generatedValues[0]];
            SelectedSale.InitialCost = generatedValues[1];
            SelectedSale.Discount = generatedValues[2];
            RaisePropertyChanged(nameof(SelectedSale));
        }
#endif

        /// <summary>
        /// Пересоздать список скидок
        /// </summary>
        private void RefreshList()
        {
            Sales.Clear();
            Sales.Add(new PercentSale());
            Sales.Add(new CertificateSale());
            SelectedSale = Sales[0];
            RaisePropertyChanged(nameof(SelectedSale));
        }

        /// <summary>
        /// Отправка заполненного объекта в лист
        /// </summary>
        private void SendNewSaleToMainWindow()
        {
            if (!SelectedSale.HasErrors)
            {
                Messenger.Default.Send(SelectedSale);
                
                RefreshList();
            }
        }

        #endregion
    }
}
