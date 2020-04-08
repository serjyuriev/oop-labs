using System.ComponentModel;

namespace DiscountCalculatorModel
{
    /// <summary>
    /// Скидочная система
    /// </summary>
    public interface ISales : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        /// <summary>
        /// Цена товара без учета скидки
        /// </summary>
        double InitialCost { get; set; }

        /// <summary>
        /// Размер скидки
        /// </summary>
        double Discount { get; set; }

        /// <summary>
        /// Цена с учетом скидки
        /// </summary>
        double FinalCost { get; }

        /// <summary>
        /// В чем измеряется скидка
        /// </summary>
        string DiscountMeasure { get; }

        /// <summary>
        /// Название системы скидки
        /// </summary>
        string Name { get; }
    }
}
