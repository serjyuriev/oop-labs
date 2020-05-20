using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;

namespace DiscountsView.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Получить экземпляр модели представления главного окна
        /// </summary>
        public MainViewModel MainViewModel
        {
            get => ServiceLocator.Current.GetInstance<MainViewModel>();
        }

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AddingObjectViewModel>();
            SimpleIoc.Default.Register<SearchViewModel>();
        }
    }
}