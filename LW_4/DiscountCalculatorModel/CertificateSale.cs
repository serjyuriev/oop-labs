using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace DiscountCalculatorModel
{
    /// <summary>
    /// Сертификационная система скидки
    /// </summary>
    [Serializable]
    public class CertificateSale : ISales
    {
        #region Fields

        /// <summary>
        /// Размер скидки в процентах
        /// </summary>
        private double _discount;

        /// <summary>
        /// Коллекция ошибок, ключ - название свойства
        /// </summary>
        private Dictionary<string, List<string>> _errors =
            new Dictionary<string, List<string>>();

        /// <summary>
        /// Цена без учета скидки
        /// </summary>
        private double _initialCost;

        #endregion

        #region Properties

        /// <summary>
        /// Размер скидки в процентах
        /// </summary>
        public double Discount
        {
            get => _discount;
            set
            {
                if (IsInputValid(value, nameof(Discount)))
                {
                    _discount = value;
                }
            }
        }

        /// <summary>
        /// В чем измеряется скидка
        /// </summary>
        public string DiscountMeasure
        {
            get => "rub";
        }

        /// <summary>
        /// Цена с учетом скидки
        /// </summary>
        public double FinalCost
        {
            get
            {
                var finalCost = Math.Round(InitialCost - Discount, 2);

                if (finalCost < 0)
                {
                    return 0;
                }
                else
                {
                    return finalCost;
                }
            }
        }

        /// <summary>
        /// Имеются ли ошибки в данном классе
        /// </summary>
        public bool HasErrors
        {
            get => _errors.Count > 0;
        }

        /// <summary>
        /// Цена без учета скидки
        /// </summary>
        public double InitialCost
        {
            get => _initialCost;
            set
            {
                if (IsInputValid(value, nameof(InitialCost)))
                {
                    _initialCost = value;
                }
            }
        }

        /// <summary>
        /// Имя сертификационной системы
        /// </summary>
        public string Name
        {
            get => "Certificate";
        }

        #endregion

        #region Constructor
        
        /// <summary>
        /// Создать объект класса CertificateSale
        /// </summary>
        public CertificateSale() { }

        /// <summary>
        /// Создать объект класса CertificateSale
        /// </summary>
        /// <param name="initialCost">Цена без скидки</param>
        /// <param name="discount">Размер скидки в рублях</param>
        public CertificateSale(double initialCost, double discount)
        {
            InitialCost = initialCost;
            Discount = discount;
        }

        #endregion

        #region Events

        /// <summary>
        /// Событие появления/исчезновения ошибки
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Событие изменения свойства
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

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

        /// <summary>
        /// Проверка ввода на наличие ошибок
        /// </summary>
        /// <param name="value">Величина параметра</param>
        /// <param name="propertyName">Название проверяемого свойства</param>
        /// <returns></returns>
        private bool IsInputValid(double value, string propertyName)
        {
            if (value < 0)
            {
                var errors = new List<string>
                {
                    $"{propertyName} can " +
                    $"only be positive!"
                };
                SetErrors(propertyName, errors);
                return false;
            }
            else
            {
                ClearErrors(propertyName);
                OnPropertyChanged(nameof(FinalCost));
                return true;
            }
        }

        /// <summary>
        /// Запуск обновления элемента интерфейса
        /// </summary>
        /// <param name="propertyName">Название свойства, значение 
        /// которого изменилось</param>
        public void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, 
                new PropertyChangedEventArgs(propertyName));
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

        #endregion
    }
}
