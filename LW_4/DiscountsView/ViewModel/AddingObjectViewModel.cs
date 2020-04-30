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
        /// <summary>
        /// Лист с доступными системами скидок
        /// </summary>
        public IList<ISales> Sales { get; } =
            new ObservableCollection<ISales>();

        /// <summary>
        /// Initializes a new instance of the AddingObjectViewModel class
        /// </summary>
        public AddingObjectViewModel()
        {
            Sales.Add(new PercentSale());
            Sales.Add(new CertificateSale());

#if MESSENGER
            // TODO Показать такой способ вызова нового окна
            // и убедиться в его адекватности
            Messenger.Default.Register<NotificationMessage>(
                this, message =>
                {
                    if (message.Notification == "openAddingSaleWindow")
                    {
                        var view = new AddingObjectWindow();
                        view.DataContext = this;
                        view.Show();
                    }
                });
#endif
        }

        
    }
}
