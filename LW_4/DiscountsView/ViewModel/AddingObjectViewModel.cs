using DiscountCalculatorModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace DiscountsView.ViewModel
{
    /// <summary>
    /// ViewModel для окна AddingObjectWindow
    /// </summary>
    public class AddingObjectViewModel : ViewModelBase
    {
        private static Random _random = new Random();

        #region Properties

        public GridLength LastGridHeight
        {
            get
            {
#if DEBUG
                return new GridLength(1, GridUnitType.Star);
#else
                return new GridLength(0);
#endif
            }
        }

#if DEBUG
        public RelayCommand CreateRandomDataCommand { get; private set; }
#endif

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
        /// Команда на создание новой скидки
        /// </summary>
        public RelayCommand AddCreatedSale { get; private set; }

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

        /// <summary>
        /// Отправка заполненного объекта в лист
        /// </summary>
        private void SendNewSaleToMainWindow()
        {
            if (!SelectedSale.HasErrors)
            {
                Messenger.Default.Send<GenericMessage<ISales>>(
                    new GenericMessage<ISales>(SelectedSale));
                
                RefreshList();
            }
        }

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

#if DEBUG
        private void CreateRandomData()
        {
            var generatedValues = Randomizer.GetRandomValuesForSales(_random);

            SelectedSale = Sales[(int)generatedValues[0]];
            SelectedSale.InitialCost = generatedValues[1];
            SelectedSale.Discount = generatedValues[2];
            RaisePropertyChanged(nameof(SelectedSale));
        }
#endif
    }
}
