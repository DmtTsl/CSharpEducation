using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Lesson_15
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        public App()
        {
            AuthenticationVM authenticationVM = new AuthenticationVM();
            Authentication authentication = new Authentication();
            MainWindow mainWindow = new MainWindow();
            authentication.DataContext = authenticationVM;
            authentication.ShowDialog();
            if (authentication.DialogResult == true)
            {
                MainWindowVM mainWindowVM = new MainWindowVM(authenticationVM.FirstName, authenticationVM.SecondName, authenticationVM.MiddleName);

                mainWindow.DataContext = mainWindowVM;
                mainWindow.Show();

            }
            else this.Shutdown();
        }
        
        
    }
}
