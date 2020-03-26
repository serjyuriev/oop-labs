using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscountCalculatorModel;

namespace DiscountCalculatorGUI.ViewModels
{
    /// <summary>
    /// Модель представления для окна MainWindow
    /// </summary>
    public class MainWindowViewModel
    {
        /// <summary>
        /// Список систем скидок
        /// </summary>
        public IList<ISales> Sales { get; } =
            new ObservableCollection<ISales>();

        /// <summary>
        /// Конструктор модели представления
        /// </summary>
        public MainWindowViewModel()
        {
            Sales.Add(new CertificateSale());
            Sales.Add(new PercentSale());
        }
    }
}
