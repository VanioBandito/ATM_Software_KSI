using System;
using System.Collections.Generic;


public class Person 
{
    public int ID { get; }
    public string Name { get; }
    public string IBAN { get; }
    public decimal Balance { get; set; }
    private string PIN { get; set; }
    public Dictionary<string, decimal> Bills { get; }

    public Person(int id, string name)
    {
        ID = id;
        Name = name;
        Bills = new Dictionary<string, decimal>();
        IBAN = $"BG99SSSS{id:D12}";
        PIN = GenPIN(4); 
    }
    private string GenPIN(int size)
    {
        string pin = "";
        Random random = new Random();
        for (int i = 0; i < size; i++)
        {
            pin += random.Next(0, 10).ToString();
        }
        return pin;
    }

  
    public bool ChangePIN(string pin)
    {
        if (pin.Length == 4)
        {
            for (int i = 0; i < pin.Length; i++)
            {
                if (pin[i] < '0' || pin[i] > '9')
                {
                    return false; 
                }
            }
            PIN = pin;
            return true;
        }
        return false;
    }

    public string AddBill(decimal amount)
    {
        string key = Guid.NewGuid().ToString();
        Bills.Add(key, amount);
        return key;
    }
    public bool CheckPIN(string pin)
    {
        return pin == PIN;
    }
}
