#define LOCATOR

using DiscountCalculatorModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountsView.ViewModel
{
    /// <summary>
    /// ViewModel для окна AddingObjectWindow
    /// </summary>
    public class AddingObjectViewModel : ViewModelBase
    {
        //private ISales _selectedSale;

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
        /// Initializes a new instance of the AddingObjectViewModel class
        /// </summary>
        public AddingObjectViewModel()
        {
            Sales.Add(new PercentSale());
            Sales.Add(new CertificateSale());
            SelectedSale = Sales[0];
        }

        
    }
}
