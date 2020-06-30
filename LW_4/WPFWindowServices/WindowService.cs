using System.Windows;
using System.Windows.Controls;

namespace WPFWindowServices
{
    /// <summary>
    /// Создать объект класса WindowService для создания новых окон
    /// </summary>
    public static class WindowService
    {
        /// <summary>
        /// Создать новое окно на основании переданной модели представления
        /// </summary>
        /// <param name="viewModel">Модель представления для окна</param>
        /// <param name="title">Заголовок окна</param>
        public static void ShowWindow(object viewModel, string title)
        {
            var contentUI = new ContentControl
            {
                Content = viewModel
            };

            var dockPanel = new DockPanel();
            dockPanel.Children.Add(contentUI);

            var window = new Window
            {
                Content = dockPanel,

                // Настройки внешнего вида окна
                SizeToContent = SizeToContent.WidthAndHeight,
                Title = title,
                ResizeMode = ResizeMode.NoResize,
                WindowStartupLocation =
                WindowStartupLocation.CenterScreen
            };

            window.ShowDialog();
        }
    }
}
