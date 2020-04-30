#define MESSENGER

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
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        ///  оманда на открытие окна добавлени€ скидки
        /// </summary>
        public RelayCommand OpenAddingSaleWindowCommand { get; private set; }

        /// <summary>
        /// ћодель представлени€ дл€ окна добавлени€
        /// TODO “ак делать наверн€ка неправильно, нужно пон€ть,
        /// каким образом создавать модели представлени€ дл€ использовани€
        /// мессенджера
        /// </summary>
        public AddingObjectViewModel AddingObjectViewModel { get; } =
            new AddingObjectViewModel();

        /// <summary>
        /// —писок систем скидок
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
            var openAddingSaleWindow = new AddingObjectWindow();
            openAddingSaleWindow.Show();
            #elif MESSENGER
            Messenger.Default.Send(
                new NotificationMessage("openAddingSaleWindow"));
            #endif
        }
    }
}