
using System;
using SplashKitSDK;

public class Account
{
    private decimal _balance; /* a field to store the balance */
    private string _name; /* a field to store the account information */
    public string Name
    {
        get { return _name; }
    }
    public Account(string name, decimal startingBalance)
    {
        _name = name;
        _balance = startingBalance;
    }
    public bool Deposit(decimal amountToAdd) /* deposit amount that large than 0 only */
    {
        if (amountToAdd > 0)
        {
            _balance = _balance + amountToAdd;
            return true;
        }
        return false;
    }
    public bool Withdraw(decimal amountToSub) /* withdraw amount that large than 0 and less than balance */
    {
        if (amountToSub > 0 && amountToSub <= _balance)
        {
            _balance = _balance - amountToSub;
            return true;
        }
        return false;
    }
    public void Print()
    {
        Console.WriteLine($"Account owner: {_name}");
        Console.WriteLine($"Balance: {_balance}");
    }
}
