using System;
using SplashKitSDK;
using System.Collections.Generic;

public abstract class Transaction
{
    // SO the idea is, class transaction has private fields: _date and _executed and _reversed 
    // (personlly I think let child class has _reversed is a good idea but anyway) 
    // child class like withdraw have private fields: _success
    // withdraw and deposit and thransfer are able to get Executed and Reversed and DateStamp from transaction. Transaction is allowed to get Success from child objects.

    protected decimal _amount;
    private DateTime _dateStamp;
    private bool _executed = false;
    private bool _reversed = false;
    public bool Executed { get { return _executed; } }
    public bool Reversed { get { return _reversed; } }
    public abstract bool Success { get; }
    public DateTime DateStamp { get { return _dateStamp; } }
    // constructor goes here
    public Transaction(Decimal amount) { _amount = amount; }

    public virtual void Execute()
    {
        if (_executed is true)
        {
            throw new Exception("The transaction has already been completed.");
        }
        else
        {
            _executed = true;
            _dateStamp = DateTime.Now;
        }
    }
    public virtual void Rollback()
    {
        if (_executed is false)
        {
            throw new Exception("Cannot rollback this transaction since it is not done yet.");
        }
        else if (_reversed is true)
        {
            throw new Exception("The rollback has already been completed.");
        }
        else
        {
            if (this is WithdrawTransaction) (this as DepositTransaction).Execute();
            if (this is DepositTransaction) (this as WithdrawTransaction).Execute();
            _reversed = Success;
            _dateStamp = DateTime.Now;
            if (Reversed is false)
            {
                throw new Exception("The processaion has been stoped due to an issue.");
            }
        }
    }
    public abstract void Print();
}