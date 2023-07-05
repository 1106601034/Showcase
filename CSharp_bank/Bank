using System.Collections.Generic;
using System;
using SplashKitSDK;

public class Bank
{
    private List<Transaction> _transactions = new List<Transaction>();
    private List<Account> _account = new List<Account>();

    public void AddAccount(Account account)
    {
        _account.Add(account);
    }
    public Account GetAccount(string name)
    {
        foreach (var item in _account)
        {
            if (item.Name == name)
            {
                return item;
            }
        }
        return null;
    }
    public void ExecuteTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
        transaction.Execute();
    }
    public void PrintTransactionHistory()
    {
        if (_transactions.Count == 0)
        {
            Console.WriteLine("No statement be found.");
        }
        foreach (Transaction item in _transactions)
        {
            item.Print();
        }
    }
}
