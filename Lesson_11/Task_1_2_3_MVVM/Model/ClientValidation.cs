using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_2_3_MVVM
{
    public partial class Client: IDataErrorInfo
    {
        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(PhoneNumber):
                        if (!string.IsNullOrEmpty(PhoneNumber) && PhoneNumber.Length < 10)
                        {
                            error = "Номер телефона должен состоять из 10 цифр";
                        }
                        break;
                    case nameof(PassNumber):
                        if (!string.IsNullOrEmpty(PassNumber) && PassNumber.Length < 10)
                        {
                            error = "Номер паспорта должен состоять из 10 цифр";
                        }
                        break;
                }
                return error;
            }
        }
    }
}
