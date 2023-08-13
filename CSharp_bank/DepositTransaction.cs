using System;
using SplashKitSDK;

public class DepositTransaction : Transaction
{
    private Account _account;
    private bool _success = false;
    public override bool Success { get { return _success; } }
    public DepositTransaction(Account account, decimal amount) : base(amount)
    {
        _account = account;
    }
    public override void Execute()
    {
        base.Execute();
        _success = _account.Deposit(_amount);
    }
    public override void Rollback()
    {
        base.Rollback();
    }
    public override void Print() /* report details of the deposit. */
    {
        if (Executed is true)
        {
            if (_success is true)
            {
                Console.WriteLine("You made a deposit of " + _amount + " to " + _account.Name + " at " + DateStamp);
            }
            if (_success is false) /* show the amount and ask user to check after a deposit has been failed. */
            {
                Console.WriteLine("You requested to deposit " + _amount + " please check your balance and try again.");
            }
        }
        if (Reversed is true) /* report details of the return. */
        {
            Console.WriteLine(_amount + " has been returned to " + _account.Name + ".");
        }
    }
}