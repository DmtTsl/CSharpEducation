using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_2_3
{
    public class DepositAccount:Account
    {
        private decimal _Percent { get; set; }
        public DepositAccount(decimal percent) : base()
        {
            _Percent = percent;
        }
        public DepositAccount(decimal percent, decimal sum) : base()
        {
            _Percent = percent;
            this.Sum = sum;
        }
    }
}
