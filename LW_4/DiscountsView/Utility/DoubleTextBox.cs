using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace DiscountsView.Utility
{
    /// <summary>
    /// TextBox только для double
    /// </summary>
    public class DoubleTextBox : TextBox
    {
        /// <summary>
        /// Регулярное выражение для текста в TextBox
        /// </summary>
        private static readonly Regex _regex = 
            new Regex(@"\d*\.?\d{0,2}");

        /// <summary>
        /// Проверка соответствия регулярному выражению при вводе текста
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewTextInput(
            TextCompositionEventArgs e)
        {
            var fullNewText = this.Text + e.Text;

#if DEBUG
            Console.Out.WriteLine(fullNewText);
            Console.Out.WriteLine(!_regex.IsMatch(fullNewText));
            Console.Out.WriteLine(_regex.Matches(fullNewText).Count);
#endif

            if (_regex.Matches(fullNewText).Count > 2)
            {
                e.Handled = true;
            }

            base.OnPreviewTextInput(e);
        }
    }
}
