public class ATM
{
    public decimal Balance { get; set; }
    public ATM()
    {
        Balance = 0;
    }
    public bool Service(decimal value)
    {
        Balance += value;
        return true;
    }
    public bool Deposit(Person owner, decimal value)
    {
        if (value <= 0)
        {
            return false;
        }

        owner.Balance += value;
        return true;
    }
    public bool Withdraw(Person owner, decimal value)
    {
        if (value <= 0 || value > owner.Balance || value > Balance)
        {
            return false;
        }

        owner.Balance -= value;
        Balance -= value;
        return true;
    }
    public decimal GetBalance(Person owner)
    {
        return owner.Balance;
    }
    public bool ChangePIN(Person owner, string newPin)
    {
        return owner.ChangePIN(newPin);
    }
    public bool PayBill(Person owner, string billId)
    {
        if (!owner.Bills.ContainsKey(billId))
        {
            return false;
        }

        decimal billAmount = owner.Bills[billId];
        if (owner.Balance < billAmount)
        {
            return false;
        }

        owner.Balance -= billAmount;
        owner.Bills.Remove(billId); 
        return true;
    }
    public bool BankTransaction(Person owner, Person receiver, decimal value)
    {
        if (value <= 0 || value > owner.Balance)
        {
            return false; 
        }

        owner.Balance -= value;
        receiver.Balance += value;
        return true;
    }
}
