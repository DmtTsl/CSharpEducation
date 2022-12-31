
using System.Windows;

namespace Task_1_2_3
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            MainWindow mainWindow = new MainWindow();
            MainWindowVM mainWindowVM = new MainWindowVM();            
            mainWindow.DataContext = mainWindowVM;
            mainWindow.Show();
        }
        
    }
}
