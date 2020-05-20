using System.ComponentModel;

namespace DiscountCalculatorModel
{
    /// <summary>
    /// Скидочная система
    /// </summary>
    public interface ISales : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        /// <summary>
        /// Размер скидки
        /// </summary>
        double Discount { get; set; }

        /// <summary>
        /// В чем измеряется скидка
        /// </summary>
        string DiscountMeasure { get; }

        /// <summary>
        /// Цена с учетом скидки
        /// </summary>
        double FinalCost { get; }

        /// <summary>
        /// Цена товара без учета скидки
        /// </summary>
        double InitialCost { get; set; }

        /// <summary>
        /// Название системы скидки
        /// </summary>
        string Name { get; }
    }
}
