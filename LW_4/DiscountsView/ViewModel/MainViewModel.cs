using DiscountCalculatorModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using WPFWindowServices;

namespace DiscountsView.ViewModel
{
    /// <summary>
    /// ������ ������������� �������� ���� ���������
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IWindowService _windowService = new WindowService();

        /// <summary>
        /// ������ ������������� ���� ���������� �������
        /// </summary>
        private AddingObjectViewModel _addingObjectViewModel =
            new AddingObjectViewModel();

        /// <summary>
        /// ������� �� �������� ���� ���������� ������
        /// </summary>
        public RelayCommand OpenAddingSaleWindowCommand { get; private set; }

        /// <summary>
        /// ������ ������ ������
        /// </summary>
        public IList<ISales> Sales { get; } =
            new ObservableCollection<ISales>();

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
#if DEBUG
            Sales.Add(new CertificateSale(150, 50));
            Sales.Add(new CertificateSale(10130, 3699));
            Sales.Add(new PercentSale(7999, 33));
            Sales.Add(new PercentSale(785000, 13));
#endif

            OpenAddingSaleWindowCommand = new RelayCommand(
                OpenAddingSaleWindow);
        }

        private void OpenAddingSaleWindow()
        {
            _windowService.ShowWindow(
                _addingObjectViewModel, "Adding new sale");
        }
    }
}