using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DiscountCalculatorGUI.ViewModels;

namespace DiscountCalculatorGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Модель представления
        /// </summary>
        public MainWindowViewModel ViewModel { get; set; } =
            new MainWindowViewModel();

        /// <summary>
        /// Конструктор модели
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Закрыть приложение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApplicationClose_Click(
            object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Перемещение окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseDown(
            object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        /// <summary>
        /// Скрытие контента окна при открытии шторки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDrawerOpen_Click(
            object sender, RoutedEventArgs e)
        {
            ContentGrid.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Показ контента окна при закрытии шторки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDrawerClose_Click(
            object sender, RoutedEventArgs e)
        {
            ContentGrid.Visibility = Visibility.Visible;
        }
    }
}
