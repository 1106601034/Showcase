using System;
using SplashKitSDK;

public enum MenuOption
{
    NewAccount,
    Withdraw,
    Deposit,
    Transaction,
    Print,
    Print_History,
    Quit
}
public class BankProgram
{
    private static MenuOption ReadUserOption() /* a method to build a menu. */
    {
        int option;
        do
        {
            try
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("1 Create new account.");
                Console.WriteLine("2 Withdraw from your account.");
                Console.WriteLine("3 Deposit from your account.");
                Console.WriteLine("4 Make a Transaction.");
                Console.WriteLine("5 Print statement.");
                Console.WriteLine("6 Print all statement.");
                Console.WriteLine("7 Log out.");
                Console.WriteLine("-------------------------");
                option = Convert.ToInt32(Console.ReadLine());
            }
            catch (System.Exception)  /* catch error that caused by invaild input. */
            {
                Console.WriteLine("unexpected command received. Please try again.");
                option = -1;
            }
        } while (option > 7 || option < 1);
        return (MenuOption)(option - 1);
    }

    private static void AddAccount(Bank bank)
    {
        Console.WriteLine("Please tell me your full name: ");
        String name = Console.ReadLine();
        if (name.Length == 0)
        {
            Console.WriteLine("Invaild name received.");
            return;
        }
        Account account_0 = new Account(name, 0);
        bank.AddAccount(account_0);
    }
    private static Account FindAccount(Bank fromBank)
    {
        Console.Write("Enter account name: ");
        String name = Console.ReadLine();
        Account result = fromBank.GetAccount(name);
        if (result is null)
        {
            Console.WriteLine($"No account found with name {name}");
        }
        return result;
    }
    private static void DoWithdraw(Bank formBank)  /* a method to start a withdraw. */
    {
        Account formAccount = FindAccount(formBank);
        if (formBank is null) return;
        decimal input;
        do
        {
            try
            {
                Console.WriteLine("How much you would like to withdraw?");
                input = Convert.ToDecimal(Console.ReadLine());
            }
            catch (System.Exception)  /* catch error that caused by invaild input. */
            {
                Console.WriteLine("Invalid amount received. Please try again.");
                input = -1;
            }
        } while (input == -1);
        /* Create new object to process a withdraw. */
        WithdrawTransaction processing = new WithdrawTransaction(formAccount, input);
        formBank.ExecuteTransaction(processing);
        processing.Print();
    }
    private static void DoDeposit(Bank toBank) /* a method to start a deposit. */
    {
        Account toAccount = FindAccount(toBank);
        if (toAccount is null) return;
        decimal input;
        do
        {
            try
            {
                Console.WriteLine("How much you would like to Deposit?");
                input = Convert.ToDecimal(Console.ReadLine());
            }
            catch (System.Exception) /* catch error that caused by invaild input. */
            {
                Console.WriteLine("Invalid amount received. Please try again.");
                input = -1;
            }
        } while (input == -1);
        /* Create new object to process a deposit. */
        DepositTransaction processing = new DepositTransaction(toAccount, input);
        toBank.ExecuteTransaction(processing);
        processing.Print();
    }
    private static void DoTransfer(Bank bank)/* a method to start a transaction. */
    {
        Account fromAccount = FindAccount(bank);
        if (fromAccount is null) return;
        Account toAccount = FindAccount(bank);
        if (toAccount is null) return;
        decimal input;
        do
        {
            try
            {
                Console.WriteLine("How much you would like to transfer?");
                input = Convert.ToDecimal(Console.ReadLine());
            }
            catch (System.Exception)
            {
                Console.WriteLine("Invalid amount received. Please try again.");/* catch error that caused by invaild input. */
                input = -1;
            }
        } while (input == -1);
        /* Create new object to process a transaction. */
        TransferTransaction processing = new TransferTransaction(fromAccount, toAccount, input);
        bank.ExecuteTransaction(processing);
        processing.Print();
    }
    private static void DoPrint(Bank bank)/* a method to print account detail. */
    {
        Account account = FindAccount(bank);
        if (account is null) return;
        account.Print();
    }
    private static void DoPrintAll(Bank bank)/* a method to print account detail. */
    {
        bank.PrintTransactionHistory();
    }

    public static void Main()
    {
        Bank bank = new Bank();
        MenuOption userSelection;
        do
        {
            userSelection = ReadUserOption();
            switch (userSelection)
            {
                case MenuOption.NewAccount:
                    AddAccount(bank);
                    break;
                case MenuOption.Withdraw:
                    DoWithdraw(bank);
                    break;
                case MenuOption.Deposit:
                    DoDeposit(bank);
                    break;
                case MenuOption.Transaction:
                    DoTransfer(bank);
                    break;
                case MenuOption.Print:
                    DoPrint(bank);
                    break;
                case MenuOption.Print_History:
                    DoPrintAll(bank);
                    break;
            }
        } while (userSelection != MenuOption.Quit);
        Console.WriteLine("Closing program...");
    }
}