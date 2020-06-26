using DiscountCalculatorModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SerializationServices;
using System;
using System.Collections;
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
        private string _pathToSaveFile;

        /// <summary>
        /// ������ ��� �������� ����
        /// </summary>
        private readonly IWindowService _windowService = new WindowService();

        #endregion

        #region Properties

        /// <summary>
        /// ������� �� ������������� ������
        /// </summary>
        public RelayCommand ApplyCommand { get; private set; }

        /// <summary>
        /// ������� �� ������� ����� ���� �� ��������� ������
        /// </summary>
        public RelayCommand ClearDataCommand { get; private set; }

        /// <summary>
        /// ������ ����� �� ������� ������
        /// </summary>
        public bool IsDiscountEnabled { get; set; }

        /// <summary>
        /// ������ ����� �� �������� ����
        /// </summary>
        public bool IsInitialCostEnabled { get; set; }

        /// <summary>
        /// ������ ����� �� ���� ������
        /// </summary>
        public bool IsSaleSystemEnabled { get; set; }

        /// <summary>
        /// ������� �� �������� ������
        /// </summary>
        public RelayCommand LoadSalesCommand { get; private set; }

        /// <summary>
        /// ������� �� �������� ���� ���������� ������
        /// </summary>
        public RelayCommand OpenAddingSaleWindowCommand { get; private set; }

        /// <summary>
        /// ��������� ������� ������ ��� ������
        /// </summary>
        public List<ISales> PossibleSalesSystems { get; private set; } =
            new List<ISales>() { new CertificateSale(), new PercentSale() };

        /// <summary>
        /// ������� �� �������� ��������� ������ �� �������
        /// </summary>
        public RelayCommand<IList> RemoveSaleCommand { get; private set; }

        /// <summary>
        /// ������ ������ ������
        /// </summary>
        public IList<ISales> Sales { get; private set; } =
            new ObservableCollection<ISales>();

        /// <summary>
        /// ������ ��� ���������� ��������� �����
        /// </summary>
        public ISales SaleToFill { get; private set; }

        /// <summary>
        /// ������� �� ���������� ������
        /// </summary>
        public RelayCommand SaveSalesCommand { get; private set; }
        
        /// <summary>
        /// ������ ������ ��� ������
        /// </summary>
        public IList<ISales> SearchedSales { get; private set; } =
            new ObservableCollection<ISales>();

        /// <summary>
        /// ��������� � ������ ������
        /// </summary>
        public ISales SelectedSale { get; set; }

        /// <summary>
        /// ��������� � ������ ������ ������
        /// </summary>
        public ISales SelectedSearchedSale { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            SaleToFill = new CertificateSale();
            SelectedSearchedSale = PossibleSalesSystems[0];

#if DEBUG
            Sales.Add(new CertificateSale(150, 50));
            Sales.Add(new CertificateSale(10130, 3699));
            Sales.Add(new PercentSale(7999, 33));
            Sales.Add(new PercentSale(785000, 13));
#endif

            ApplyCommand = new RelayCommand(Apply);
            ClearDataCommand = new RelayCommand(ClearData);
            OpenAddingSaleWindowCommand = new RelayCommand(
                OpenAddingSaleWindow);
            RemoveSaleCommand = new RelayCommand<IList>(RemoveSale);
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
        /// ����������� ����� �� �������� ����������
        /// </summary>
        private void Apply()
        {
            SearchedSales.Clear();

            if (IsSaleSystemEnabled)
            {
                if (IsInitialCostEnabled)
                {
                    if (IsDiscountEnabled)
                    {
                        foreach (ISales sale in Sales)
                        {
                            if (sale.GetType() == 
                                SelectedSearchedSale.GetType() &&
                                sale.InitialCost == SaleToFill.InitialCost &&
                                sale.Discount == SaleToFill.Discount)
                            {
                                SearchedSales.Add(sale);
                            }
                        }
                    }
                    else
                    {
                        foreach (ISales sale in Sales)
                        {
                            if (sale.GetType() == 
                                SelectedSearchedSale.GetType() &&
                                sale.InitialCost == SaleToFill.InitialCost)
                            {
                                SearchedSales.Add(sale);
                            }
                        }
                    }
                }
                else
                {
                    if (IsDiscountEnabled)
                    {
                        foreach (ISales sale in Sales)
                        {
                            if (sale.GetType() == 
                                SelectedSearchedSale.GetType() &&
                                sale.Discount == SaleToFill.Discount)
                            {
                                SearchedSales.Add(sale);
                            }
                        }
                    }
                    else
                    {
                        foreach (ISales sale in Sales)
                        {
                            if (sale.GetType() == 
                                SelectedSearchedSale.GetType())
                            {
                                SearchedSales.Add(sale);
                            }
                        }
                    }
                }
            }
            else
            {
                if (IsInitialCostEnabled)
                {
                    if (IsDiscountEnabled)
                    {
                        foreach (ISales sale in Sales)
                        {
                            if (sale.InitialCost == SaleToFill.InitialCost &&
                                sale.Discount == SaleToFill.Discount)
                            {
                                SearchedSales.Add(sale);
                            }
                        }
                    }
                    else
                    {
                        foreach (ISales sale in Sales)
                        {
                            if (sale.InitialCost == SaleToFill.InitialCost)
                            {
                                SearchedSales.Add(sale);
                            }
                        }
                    }
                }
                else
                {
                    if (IsDiscountEnabled)
                    {
                        foreach (ISales sale in Sales)
                        {
                            if (sale.Discount == SaleToFill.Discount)
                            {
                                SearchedSales.Add(sale);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// �������� ���� ������
        /// </summary>
        private void ClearData()
        {
            SaleToFill = new CertificateSale();
            RaisePropertyChanged(nameof(SaleToFill));
        }

        /// <summary>
        /// ��������� ������ �� �����
        /// </summary>
        private void LoadSales()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "svy files (.svy)|*.svy";
            dialog.Title = "Opening list";
            dialog.ShowDialog();

            if (dialog.FileName == String.Empty)
            {
                return;
            }

            _pathToSaveFile = dialog.FileName;
            try
            {
                Sales = new ObservableCollection<ISales>(
                    Serializer.DeserializeData<ISales>(_pathToSaveFile));
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
            catch (System.Runtime.Serialization.SerializationException)
            {
                MessageBox.Show("���� ���������!",
                    "������", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (System.Text.DecoderFallbackException)
            {
                MessageBox.Show("���� ���������!",
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
        /// �������� ������
        /// </summary>
        /// <param name="items">��������� ������</param>
        private void RemoveSale(object items)
        {
            var selection = items as IList;
            var count = selection.Count;
            var counter = 0;

            while (counter < count)
            {
                Sales.Remove(selection[0] as ISales);
                counter++;
            }
            
            RaisePropertyChanged(nameof(Sales));
        }

        /// <summary>
        /// ��������� ������ � ����
        /// </summary>
        private void SaveSales()
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = "svy files (.svy)|*.svy";
            dialog.Title = "Saving list";
            dialog.ShowDialog();

            if (dialog.FileName == String.Empty)
            {
                return;
            }

            _pathToSaveFile = dialog.FileName;
            Serializer.SerializeData<ISales>(
                new List<ISales>(Sales), _pathToSaveFile);
        }

        #endregion
    }
}