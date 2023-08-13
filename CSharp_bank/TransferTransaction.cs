using System;
using SplashKitSDK;

public class TransferTransaction : Transaction
{
    private Account _toAccount; /* a field to store the deposit account information */
    private Account _fromAccount; /* a field to store the withdraw account information */
    private DepositTransaction _depositTransaction; 
    private WithdrawTransaction _withdrawTransaction;
    private bool _success = false;
    public override bool Success { get { return _success; } }

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount)  : base(amount)/* get values and then store into local fields */
    {
        _fromAccount = fromAccount;
        _toAccount = toAccount;
        _withdrawTransaction = new WithdrawTransaction(_fromAccount, _amount);
        _depositTransaction = new DepositTransaction(_toAccount, _amount);
    }
    public override void Execute()
    {
        base.Execute();
        _withdrawTransaction.Execute();
        _withdrawTransaction.Print();
        if (_withdrawTransaction.Success is true)
        {
            _depositTransaction.Execute();
            _depositTransaction.Print();
            if (_depositTransaction.Success is false)
            {
                Rollback();
            }
            else if (_depositTransaction.Success is true)
            {
                _success = true;
            }
        }
    }
    public override void Rollback()
    {
        if (Executed is false) /* end rollback if the transfer has never been done. */
        {
            throw new Exception("Cannot rollback this transaction since it is not done yet.");
        }
        if (Reversed is true)
        {
            throw new Exception("The reversation has been completed.");
        }
        if (_withdrawTransaction.Success is true) /* undo withdraw and deposit if any of them has been successed */
        {
            _withdrawTransaction.Rollback();
            _withdrawTransaction.Print();
        }
        if (_depositTransaction.Success is true)
        {
            _depositTransaction.Rollback();
            _depositTransaction.Print();
        }
        if (Reversed is false)
        {
            throw new Exception("The processaion has been stoped due to an issue.");
        }
    }
    public override void Print() /* report details of the transfer. */
    {
        if (Executed is true)
        {
            if (_success is true)
            {
                Console.WriteLine("You made a transfer of " + _amount + " from " + _fromAccount.Name + " to " + _toAccount.Name + " at " + DateStamp);
            }
            if (_success is false)
            {
                Console.WriteLine("You requested to transfer " + _amount + " has failed.");
            }
        }
        if (Reversed is true)
        {
            Console.WriteLine(_amount + " has been returned from " + _fromAccount.Name + " to " + _toAccount.Name + ".");
        }
    }
}