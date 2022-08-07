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
using System.Windows.Shapes;

namespace Task
{
    /// <summary>
    /// Логика взаимодействия для AddAccountWindow.xaml
    /// </summary>
    public partial class AddAccountWindow : Window
    {
        public AddAccountWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddAccountWindowVM vm = (AddAccountWindowVM)this.DataContext;
            MessageBox.Show(vm.Percent.ToString());
        }
    }
}
