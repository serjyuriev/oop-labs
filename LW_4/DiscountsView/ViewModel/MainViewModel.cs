#define LOCATOR

using DiscountCalculatorModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Policy;
using System.Windows;

namespace DiscountsView.ViewModel
{
    /// <summary>
    /// Модель представления главного окна программы
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Текущая модель представления
        /// </summary>
        private ViewModelBase _currentViewModel;

        /// <summary>
        /// Текущая модель представления
        /// </summary>
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                if (_currentViewModel == value)
                {
                    return;
                }

                _currentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

        /// <summary>
        /// Модель представления окна добавления объекта
        /// </summary>
        readonly static AddingObjectViewModel _addingObjectViewModel =
            new AddingObjectViewModel();

        /// <summary>
        /// Команда на открытие окна добавления скидки
        /// </summary>
        public RelayCommand OpenAddingSaleWindowCommand { get; private set; }

        /// <summary>
        /// Список систем скидок
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
#if STRAIGHT
            // Так нельзя делать - нарушение MVVM
            var openAddingSaleWindow = new AddingObjectWindow();
            openAddingSaleWindow.Show();
#elif MESSENGER
            Messenger.Default.Send(
                new NotificationMessage("openAddingSaleWindow"));
#elif LOCATOR
            CurrentViewModel = _addingObjectViewModel;
#endif
        }
    }
}