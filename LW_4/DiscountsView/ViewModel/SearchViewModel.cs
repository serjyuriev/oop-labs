using DiscountCalculatorModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;

namespace DiscountsView.ViewModel
{
    /// <summary>
    /// Модель представления для окна поиска
    /// </summary>
    public class SearchViewModel : ViewModelBase
    {
        /// <summary>
        /// Исходный лист скидок на момент открытия окна
        /// </summary>
        private IList<ISales> _initialSales;

        #region Properties
        
        /// <summary>
        /// Команда на осуществление поиска
        /// </summary>
        public RelayCommand ApplyCommand { get; private set; }

        /// <summary>
        /// Команда на отмену результатов поиска
        /// </summary>
        public RelayCommand CancelSearchCommand { get; private set; }

        /// <summary>
        /// Команда на очистку полей окна от введенных данных
        /// </summary>
        public RelayCommand ClearDataCommand { get; private set; }

        /// <summary>
        /// Текущий лист скидок
        /// </summary>
        public IList<ISales> CurrentSales { get; private set; }

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
        /// Список возможных типов скидки
        /// </summary>
        public List<ISales> Sales { get; private set; } =
            new List<ISales>();

        /// <summary>
        /// Скидка для заполнения поисковых полей
        /// </summary>
        public ISales SaleToFill { get; private set; }

        /// <summary>
        /// Выбранный тип скидки в окне поиска
        /// </summary>
        public ISales SelectedSale { get; set; }

        #endregion

        /// <summary>
        /// Модель представления для окна поиска
        /// </summary>
        public SearchViewModel()
        {
            SaleToFill = new CertificateSale();

            Sales.Add(new PercentSale());
            Sales.Add(new CertificateSale());
            SelectedSale = Sales[0];

            ApplyCommand = new RelayCommand(Apply);
            CancelSearchCommand = new RelayCommand(CancelSearch);
            ClearDataCommand = new RelayCommand(ClearData);

            // Подписка на рассылку текущего списка скидок
            Messenger.Default.Register<IList<ISales>>(
                this, ReceiveCurrentSales);
        }

        #region Methods

        /// <summary>
        /// Осуществить поиск по заданным параметрам
        /// </summary>
        private void Apply()
        {
            if (IsSaleSystemEnabled)
            {
                foreach (ISales sale in _initialSales)
                {
                    if (sale.GetType() != SelectedSale.GetType())
                    {
                        CurrentSales.Remove(sale);
                    }
                }
            }

            if (IsInitialCostEnabled)
            {
                foreach (ISales sale in _initialSales)
                {
                    if (sale.InitialCost != SaleToFill.InitialCost)
                    {
                        CurrentSales.Remove(sale);
                    }
                }
            }

            if (IsDiscountEnabled)
            {
                foreach (ISales sale in _initialSales)
                {
                    if (sale.Discount != SaleToFill.Discount)
                    {
                        CurrentSales.Remove(sale);
                    }
                }
            }

            if (!IsSaleSystemEnabled && !IsInitialCostEnabled && 
                !IsDiscountEnabled)
            {
                CancelSearch();
            }
        }

        /// <summary>
        /// Отменить результаты поиска
        /// </summary>
        private void CancelSearch()
        {
            var isInside = false;

            for (int i = 0; i < _initialSales.Count; i++)
            {
                for (int j = 0; j < CurrentSales.Count; j++)
                {
                    if (_initialSales[i] == CurrentSales[j])
                    {
                        isInside = true;
                        break;
                    }
                }

                if (isInside)
                {
                    isInside = false;
                }
                else
                {
                    CurrentSales.Add(_initialSales[i]);
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
        /// Принять текущий лист скидок
        /// </summary>
        /// <param name="sales">Лист скидок из сообщения</param>
        private void ReceiveCurrentSales(IList<ISales> sales)
        {
            CurrentSales = sales;
            _initialSales = new List<ISales>(CurrentSales);
        }

        #endregion
    }
}
