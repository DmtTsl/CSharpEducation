namespace Task_1_2_3
{
    public class DepositAccount:Account
    {
        public decimal _Percent { get; set; }
        public DepositAccount() : base() { }       
        public DepositAccount(decimal percent, decimal sum) : base(true)
        {
            _Percent = percent;
            this.AccountSum = sum;
        }
    }
}
