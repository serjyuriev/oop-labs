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
        /// Выбранная скидочная система
        /// </summary>
        private ISales _selectedSale;

        /// <summary>
        /// Список систем скидок
        /// </summary>
        public IList<ISales> Sales { get; } =
            new ObservableCollection<ISales>();

        /// <summary>
        /// Выбранная скидочная система
        /// </summary>
        public ISales SelectedSale
        {
            get => _selectedSale;
            set => _selectedSale = value;
        }

        /// <summary>
        /// Конструктор модели представления
        /// </summary>
        public MainWindowViewModel()
        {
            Sales.Add(new CertificateSale());
            Sales.Add(new PercentSale());
            SelectedSale = Sales[0];
        }
    }
}
