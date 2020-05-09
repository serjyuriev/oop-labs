using DiscountCalculatorModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
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
        #region Fields

        /// <summary>
        /// ������ ��� �������� ����
        /// </summary>
        private readonly IWindowService _windowService = new WindowService();

        /// <summary>
        /// ������ ������������� ���� ���������� �������
        /// </summary>
        private AddingObjectViewModel _addingObjectViewModel =
            new AddingObjectViewModel();

        /// <summary>
        /// ������ ������������� ���� ������
        /// </summary>
        private SearchViewModel _searchViewModel = new SearchViewModel();

        /// <summary>
        /// ��������� � ������ ������
        /// </summary>
        private ISales _selectedSale;

        #endregion

        #region Properties

        /// <summary>
        /// ������� �� �������� ���� ���������� ������
        /// </summary>
        public RelayCommand OpenAddingSaleWindowCommand { get; private set; }

        /// <summary>
        /// ������� �� �������� ���� ������ ������
        /// </summary>
        public RelayCommand OpenSearchWindowCommand { get; private set; }

        /// <summary>
        /// ������� �� �������� ��������� ������ �� �������
        /// </summary>
        public RelayCommand RemoveSaleCommand { get; private set; }

        /// <summary>
        /// ������ ������ ������
        /// </summary>
        public IList<ISales> Sales { get; } =
            new ObservableCollection<ISales>();

        /// <summary>
        /// ��������� � ������ ������
        /// </summary>
        public ISales SelectedSale
        {
            get => _selectedSale;
            set
            {
                _selectedSale = value;
                RaisePropertyChanged(nameof(SelectedSale));
            }
        }

        #endregion

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
            OpenSearchWindowCommand = new RelayCommand(OpenSearchWindow);
            RemoveSaleCommand = new RelayCommand(RemoveSale);

            Messenger.Default.Register<GenericMessage<ISales>>(
                this, AddSaleIntoList);
        }

        #region Methods

        /// <summary>
        /// �������� ���� "Adding new sale"
        /// </summary>
        private void OpenAddingSaleWindow()
        {
            _windowService.ShowWindow(
                _addingObjectViewModel, "Adding new sale");
        }

        /// <summary>
        /// �������� ���� "Search sales"
        /// </summary>
        private void OpenSearchWindow()
        {
            _windowService.ShowWindow(
                _searchViewModel, "Search sales");
        }

        /// <summary>
        /// ���������� � ������ ����� ������
        /// </summary>
        /// <param name="sale">��������� �� VM ���� ���������� ������</param>
        private void AddSaleIntoList(GenericMessage<ISales> sale)
        {
            Sales.Add(sale.Content);
        }

        private void RemoveSale()
        {
            if (SelectedSale is object)
            {
                Sales.Remove(SelectedSale);
                RaisePropertyChanged(nameof(Sales));
            }
        }

        #endregion
    }
}