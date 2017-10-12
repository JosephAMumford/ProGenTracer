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
using ProGenTracer.Utilities;

namespace ProGenTracer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NormalizeVectorButton(object sender, RoutedEventArgs e)
        {
            //BindingExpression xBind = VectorXTextBox.GetBindingExpression(TextBox.TextProperty);
            //BindingExpression yBind = VectorYTextBox.GetBindingExpression(TextBox.TextProperty);
            //BindingExpression zBind = VectorZTextBox.GetBindingExpression(TextBox.TextProperty);
            //BindingExpression norm = NormalizedVectorUpdate.GetBindingExpression(TextBox.TextProperty);
            Vector3 a = new Vector3(double.Parse(VectorXTextBox.Text), double.Parse(VectorYTextBox.Text), double.Parse(VectorZTextBox.Text));
            a.Normalize();
            NormalizedVectorUpdate.Content = a.ToString();
        }
    }
}
