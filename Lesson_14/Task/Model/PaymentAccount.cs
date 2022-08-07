namespace Task
{
    public class PaymentAccount:Account
    {
        public PaymentAccount() : base() { }  
        public PaymentAccount(decimal sum) : base(true) 
        {
            this.AccountSum = sum;
        }
    }
}
