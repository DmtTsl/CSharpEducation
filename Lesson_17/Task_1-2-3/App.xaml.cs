﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
            MainWindowVM mainWindowVM = new MainWindowVM();
            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = mainWindowVM;
            mainWindow.Show();
        }
        
    }
}
