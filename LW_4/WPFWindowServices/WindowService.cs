using System.Windows;
using System.Windows.Controls;

namespace WPFWindowServices
{
    /// <summary>
    /// Создать объект класса WindowService для создания новых окон
    /// </summary>
    public class WindowService : IWindowService
    {
        /// <summary>
        /// Создать новое окно на основании переданной модели представления
        /// </summary>
        /// <param name="viewModel">Модель представления для окна</param>
        /// <param name="title">Заголовок окна</param>
        public void ShowWindow(object viewModel, string title)
        {
            var contentUI = new ContentControl();
            contentUI.Content = viewModel;

            var dockPanel = new DockPanel();
            dockPanel.Children.Add(contentUI);

            var window = new Window();
            window.Content = dockPanel;

            // Настройки внешнего вида окна
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.Title = title;
            window.ResizeMode = ResizeMode.NoResize;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            window.Show();
        }
    }
}
