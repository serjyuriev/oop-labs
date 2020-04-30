using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFWindowServices
{
    /// <summary>
    /// WPF MVVM ViewModel-First Services
    /// </summary>
    public interface IWindowService
    {
        /// <summary>
        /// Показать окно
        /// </summary>
        /// <param name="viewModel">Модель представления для окна</param>
        void ShowWindow(object viewModel);
    }
}
