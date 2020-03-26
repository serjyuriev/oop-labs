using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
