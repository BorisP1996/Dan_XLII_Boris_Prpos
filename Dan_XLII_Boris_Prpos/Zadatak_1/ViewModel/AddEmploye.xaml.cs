using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    /// <summary>
    /// Interaction logic for AddEmploye.xaml
    /// </summary>
    public partial class AddEmploye : Window
    {
        public AddEmploye()
        {
            InitializeComponent();
            this.DataContext = new AddEmployeViewModel(this);
        }
        private void LettersValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        /// <summary>
        /// Method allows only numbers in text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumbersOnlyTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
