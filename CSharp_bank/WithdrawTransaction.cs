using System;
using SplashKitSDK;

public class WithdrawTransaction : Transaction
{
    private Account _account;
    private bool _success = false;
    public override bool Success { get { return _success; } }
    public WithdrawTransaction(Account account, decimal amount) : base(amount)
    {
        _account = account;
    }
    public override void Execute()
    {
        base.Execute();
        _success = _account.Withdraw(_amount);
    }
    public override void Rollback()
    {
        base.Rollback();
    }
    public override void Print()
    {
        if (Executed is true)
        {
            if (_success is true)
            {
                Console.WriteLine("You made a withdraw of " + _amount + " from " + _account.Name + " at " + DateStamp);
            }
            if (_success is false)
            {
                Console.WriteLine("You requested to withdraw " + _amount + " please check your balance and try again.");
            }
        }
        if (Reversed is true)
        {
            Console.WriteLine(_amount + " has been returned to " + _account.Name + ".");
        }
    }
}