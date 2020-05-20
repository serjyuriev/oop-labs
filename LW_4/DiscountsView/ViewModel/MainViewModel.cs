using DiscountCalculatorModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SerializationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using WPFWindowServices;

namespace DiscountsView.ViewModel
{
    /// <summary>
    /// ������ ������������� �������� ���� ���������
    /// </summary>
    [Serializable]
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// ������ ������������� ���� ���������� �������
        /// </summary>
        private readonly AddingObjectViewModel _addingObjectViewModel =
            new AddingObjectViewModel();

        /// <summary>
        /// ���� �� ����� ���������� �����
        /// </summary>
        private readonly string _pathToDataFolder = 
            $@"{Environment.GetFolderPath(
                Environment.SpecialFolder.Desktop)}\Sales";

        /// <summary>
        /// ������ ������������� ���� ������
        /// </summary>
        private readonly SearchViewModel _searchViewModel =
            new SearchViewModel();

        /// <summary>
        /// ������ ��� �������� ����
        /// </summary>
        private readonly IWindowService _windowService = new WindowService();

        #endregion

        #region Properties

        /// <summary>
        /// ������� �� �������� ������
        /// </summary>
        public RelayCommand LoadSalesCommand { get; private set; }

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
        public IList<ISales> Sales { get; private set; } =
            new ObservableCollection<ISales>();

        /// <summary>
        /// ������� �� ���������� ������
        /// </summary>
        public RelayCommand SaveSalesCommand { get; private set; }

        /// <summary>
        /// ��������� � ������ ������
        /// </summary>
        public ISales SelectedSale { get; set; }

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
            SaveSalesCommand = new RelayCommand(SaveSales);
            LoadSalesCommand = new RelayCommand(LoadSales);

            // ����������� ��������� � ���������� ����� ������
            Messenger.Default.Register<ISales>(
                this, AddSaleIntoList);
        }

        #region Methods

        /// <summary>
        /// ���������� � ������ ����� ������
        /// </summary>
        /// <param name="sale">��������� �� VM ���� ���������� ������</param>
        private void AddSaleIntoList(ISales sale)
        {
            Sales.Add(sale);
        }

        /// <summary>
        /// ��������� ������ �� �����
        /// </summary>
        private void LoadSales()
        {
            try
            {
                Sales = new ObservableCollection<ISales>(
                    Serializer.DeserializeData<ISales>(
                        System.IO.Directory.GetFiles(_pathToDataFolder)[0]));
                RaisePropertyChanged(nameof(Sales));
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("����� �� ����������!",
                    "������", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("���������� �� ����������!",
                    "������", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

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
            Messenger.Default.Send(Sales);
            _windowService.ShowWindow(
                _searchViewModel, "Search sales");
        }

        /// <summary>
        /// �������� ������
        /// </summary>
        private void RemoveSale()
        {
            if (SelectedSale is object)
            {
                Sales.Remove(SelectedSale);
                RaisePropertyChanged(nameof(Sales));
            }
        }

        /// <summary>
        /// ��������� ������ � ����
        /// </summary>
        private void SaveSales()
        {
            if (!Directory.Exists(_pathToDataFolder))
            {
                Directory.CreateDirectory(_pathToDataFolder);
            }

            Serializer.SerializeData<ISales>(
                new List<ISales>(Sales), 
                $@"{_pathToDataFolder}\test.ads");
        }

        #endregion
    }
}