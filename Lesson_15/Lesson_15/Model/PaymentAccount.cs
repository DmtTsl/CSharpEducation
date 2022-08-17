namespace Lesson_15
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
