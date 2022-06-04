using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Task_1_2_3_MVVM
{
    public class MainWindowCommands
    {
        private RelayCommand _phoneLostFocus;
        public RelayCommand PhoneLostFocus
        {
            get
            {
                return _phoneLostFocus ??
                    (_phoneLostFocus = new RelayCommand(obj =>
                    {
                        
                    }));
            }
        }
        private RelayCommand _phoneGotFocus;
        public RelayCommand PhoneGotFocus
        {
            get
            {
                return _phoneGotFocus ??
                    (_phoneGotFocus = new RelayCommand(obj =>
                    {

                    }));
            }
        }
    }
}
