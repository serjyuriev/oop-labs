using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace DiscountCalculatorModel
{
    /// <summary>
    /// Процентная система скидки
    /// </summary>
    public class PercentSale : ISales
    {
        #region Fields

        /// <summary>
        /// Цена без учета скидки
        /// </summary>
        private double _initialCost;

        /// <summary>
        /// Размер скидки в процентах
        /// </summary>
        private double _discount;

        /// <summary>
        /// Коллекция ошибок, ключ - название свойства
        /// </summary>
        private Dictionary<string, List<string>> _errors =
            new Dictionary<string, List<string>>();

        #endregion

        #region Properties

        /// <summary>
        /// Цена без учета скидки
        /// </summary>
        public double InitialCost
        {
            get => _initialCost;
            set
            {
                if (value < 0)
                {
                    var errors = new List<string>
                    {
                        $"{nameof(InitialCost)} can " +
                        $"only be positive!"
                    };
                    SetErrors(nameof(InitialCost), errors);
                }
                else
                {
                    _initialCost = value;
                    ClearErrors(nameof(InitialCost));
                    OnPropertyChanged(nameof(FinalCost));
                }
            }
        }

        /// <summary>
        /// Размер скидки в процентах
        /// </summary>
        public double Discount
        {
            get => _discount;
            set
            {
                if (value < 0 || value > 100)
                {
                    var errors = new List<string>
                    {
                        $"{nameof(Discount)} value lies " +
                        $"between 0 and 100!"
                    };
                    SetErrors(nameof(Discount), errors);
                }
                else
                {
                    _discount = value;
                    OnPropertyChanged(nameof(FinalCost));
                    ClearErrors(nameof(Discount));
                }
            }
        }

        /// <summary>
        /// В чем измеряется скидка
        /// </summary>
        public string DiscountMeasure
        {
            get => "%";
        }

        /// <summary>
        /// Цена с учетом скидки
        /// </summary>
        public double FinalCost
        {
            get
            {
                return InitialCost * (1 - Discount / 100);
            }
        }

        /// <summary>
        /// Имя системы скидок
        /// </summary>
        public string Name
        {
            get => "Percent";
        }

        /// <summary>
        /// Имеются ли ошибки в данном классе
        /// </summary>
        public bool HasErrors
        {
            get => _errors.Count > 0;
        }

        #endregion

        #region Constructor
        
        /// <summary>
        /// Создать экземпляр класса PercentSale
        /// </summary>
        public PercentSale() { }

        /// <summary>
        /// Создать объект класса PercentSale
        /// </summary>
        /// <param name="initialCost">Цена без скидки</param>
        /// <param name="discount">Размер скидки в процентах</param>
        public PercentSale(double initialCost, double discount)
        {
            InitialCost = initialCost;
            Discount = discount;
        }

        #endregion

        #region Events

        /// <summary>
        /// Событие изменения свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Событие появления/исчезновения ошибки
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        #endregion

        #region Methods

        /// <summary>
        /// Запуск обновления элемента интерфейса?
        /// </summary>
        /// <param name="propertyName">Название свойства, в котором
        /// произошло изменение</param>
        public void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(
                this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Добавить ошибки в коллекцию
        /// </summary>
        /// <param name="propertyName">Название свойства, в котором
        /// возникла ошибка</param>
        /// <param name="propertyErrors">Список с ошибками 
        /// данного свойства</param>
        private void SetErrors(string propertyName, 
            List<string> propertyErrors)
        {
            _errors.Remove(propertyName);
            _errors.Add(propertyName, propertyErrors);

            ErrorsChanged?.Invoke(
                this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Очистить коллекцию ошибок от списка ошибок указанного свойства
        /// </summary>
        /// <param name="propertyName">Название свойства</param>
        private void ClearErrors(string propertyName)
        {
            _errors.Remove(propertyName);

            ErrorsChanged?.Invoke(
                this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Получить список ошибок для указанного/всех свойства(-в)
        /// </summary>
        /// <param name="propertyName">Название свойства</param>
        /// <returns>Список ошибок, если они есть.
        /// Otherwise - null</returns>
        public IEnumerable GetErrors(string propertyName = "")
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return (_errors.Values);
            }
            else
            {
                if (_errors.ContainsKey(propertyName))
                {
                    return _errors[propertyName];
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion
    }
}
