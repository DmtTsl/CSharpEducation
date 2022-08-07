using System;

namespace Task
{
    public abstract class Log
    {
        public DateTime LogDateTime { get; set; }
        public string ClientToLogName { get; set; }       
        public string WhoChanged { get; set; }
        public int? AccNum { get; set; }
        public abstract string LogEntry(int? account);
        public abstract string Message(int? account);
        public Log(string client, string changer, int? account = null)
        {
            LogDateTime = DateTime.Now;
            ClientToLogName = client;
            WhoChanged = changer;
            AccNum = account;
        }
        public Log() { }
    }
    public class ClientInfoLog : Log
    {
        public ClientChange TypeOfChange { get; set; }

        public override string LogEntry(int? account = null)
        {
            switch ((int)TypeOfChange)
            {
                case 0:
                    return $"Дата/Время: {LogDateTime.ToString()}\nВнес изменения: {WhoChanged}\nКлиент: {ClientToLogName}" +
            $"\nТип изменения {TypeOfChange}";
                case 1: goto case 0;
                case 2:
                    return $"Дата/Время: {LogDateTime.ToString()}\nВнес изменения: {WhoChanged}\nКлиент: {ClientToLogName}" +
            $"\nТип изменения {TypeOfChange}\nНомер счета {account:D7}";
                case 3: goto case 2;
                default: return string.Empty;
            }
        }
        public override string Message(int? account = null)
        {
            switch ((int)TypeOfChange)
            {
                case 0: return $"Создан клиент {ClientToLogName}";
                case 1: return $"Клиент {ClientToLogName} удален";
                case 2: return $"Открыт счет №{account:D7} для клиента {ClientToLogName}";
                case 3: return $"Закрыт счет №{account:D7} для клиента {ClientToLogName}";
                default: return string.Empty;
            }
        }
        public ClientInfoLog(string client, string changer, ClientChange clientChange, int? accountNumber = null) : base(client, changer, accountNumber)
        {
            TypeOfChange = clientChange;
        }
        public ClientInfoLog() { }
    }
    public enum ClientChange
    {
        Создание,
        Удаление,
        Открытие_счета,
        Закрытие_счета
    }
    public class ClientAccountLog : Log
    {
        public int AccountNumber { get; set; }
        public AccountChange ActionToAccount { get; set; }
        public decimal Sum { get; set; }
        public override string LogEntry(int? account=null)
        {
            switch ((int)ActionToAccount)
            {
                case 0: return $"Дата/Время: {LogDateTime.ToString()}\nВнес изменения: {WhoChanged}\nКлиент: {ClientToLogName}" +
                    $"\nДействие: {ActionToAccount}\nСчет: {AccountNumber:D7}\nСумма: {Sum}";
                case 1: goto case 0;
                case 2: return $"Дата/Время: {LogDateTime.ToString()}\nВнес изменения: {WhoChanged}\nКлиент: {ClientToLogName}" +
                    $"\nДействие: {ActionToAccount}\nСчет списания: {AccountNumber:D7}\nСчет пополнения: {account:D7}\nСумма: {Sum}";
                default: return string.Empty;
            }
                     
        }
        public override string Message(int? account = null)
        {           
            switch ((int)ActionToAccount)
            {
                case 0: return $"На счет номер {AccountNumber:D7} внесено {Sum} уе";                    
                case 1: return $"С счета номер {AccountNumber:D7} списано {Sum} уе";                    
                case 2: return $"С счета номер {AccountNumber:D7} переведено {Sum} уе на счет {account:D7}";
                    default: return string.Empty;                   
            } 
        }
        public ClientAccountLog(string client, string changer, int account, AccountChange action, decimal sum, int? accountNumber = null) : base(client, changer, accountNumber)
        {
            AccountNumber = account;
            ActionToAccount = action;
            Sum = sum;
        }
        public ClientAccountLog() { }
    }
    public enum AccountChange
    {
        Пополнение,
        Списание,
        Перевод
    }
}
