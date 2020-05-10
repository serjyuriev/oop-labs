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
using System.Windows;
namespace DiscountsView.ViewModel
{
    public class SearchViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Команда на очистку введенных данных
        /// </summary>
        public RelayCommand ClearDataCommand { get; private set; }

        /// <summary>
        /// Возможные типы скидок
        /// </summary>
        public List<string> SaleTypes { get; set; } = new List<string>();

        /// <summary>
        /// Индекс выбранной системы
        /// </summary>
        public int SelectedIndex { get; set; }

        public string InitialCost { get; set; }

        public string Discount { get; set; }

        #endregion

        public SearchViewModel()
        {
            SaleTypes.Add("Percent");
            SaleTypes.Add("Certificate");
            SelectedIndex = -1;

            ClearDataCommand = new RelayCommand(ClearData);
        }

        private void ClearData()
        {
            SelectedIndex = -1;
            RaisePropertyChanged(nameof(SelectedIndex));
            InitialCost = null;
            RaisePropertyChanged(nameof(InitialCost));
            Discount = null;
            RaisePropertyChanged(nameof(Discount));
        }
    }
}
