using Bankingsystem.Data;
using Bankingsystem.Models;

namespace Bankingsystem
{
    internal class Bankingtransaction
    {
        private readonly AppDbContext _context;

        public Bankingtransaction(AppDbContext context)
        {
            _context = context;
        }

        public void CreateAccount(string HolderName, string AccountNumber, decimal amount)
        {
            var account = new Accounts
            {
                AccountHolderName = HolderName,
                AccountNumber = AccountNumber
            };

            account.Deposit(amount);

            _context.Accounts.Add(account);
            _context.SaveChanges();

            Console.WriteLine("Account created successfully!");
        }

        public void DepositMoney(string AccountNumber, decimal Amount)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.AccountNumber == AccountNumber);
            if (account != null)
            {
                account.Deposit(Amount);
                var transaction = new Transaction
                {
                    AccountId = account.AccountId,
                    AccountNumber = AccountNumber,
                    TransactionType = $"{DateTime.Now } | Deposited money | Rs.{Amount}"
                };
                _context.Transaction.Add(transaction);
                _context.SaveChanges();
                CheckAccountBalance(AccountNumber);
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        public void WithdrawMoney(string AccountNumber, decimal Amount)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.AccountNumber == AccountNumber);
            if (account != null)
            {
                try
                {
                    account.Withdraw(Amount);
                    var transaction = new Transaction
                    {
                        AccountId=account.AccountId,
                        AccountNumber = AccountNumber,
                        TransactionType = $"{DateTime.Now} | Withdraw money  | Rs.{Amount} "
                    };
                    _context.Transaction.Add(transaction);
                    _context.SaveChanges();
                    CheckAccountBalance(AccountNumber);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        public void CheckAccountBalance(string AccountNumber)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.AccountNumber == AccountNumber);
            if (account != null)
            {
                Console.WriteLine($"Balance: {account.Balance:F2}");
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        public void GetTransactions(string accountNumber)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (account != null)
            {
                var transactions = _context.Transaction
                    .Where(t => t.AccountId == account.AccountId)
                    .ToList();

                if (transactions.Any())
                {
                    Console.WriteLine($"Transactions for Account Number: {accountNumber}");
                    Console.WriteLine("---------------------------------------------------------");
                    Console.WriteLine("Date and Time        |       Type      | Amount");
                    Console.WriteLine("---------------------------------------------------------");
                    foreach (var transaction in transactions)
                    {
                        Console.WriteLine($"{transaction.TransactionType}");
                    }
                    Console.WriteLine("---------------------------------------------------------");
                }
                else
                {
                    Console.WriteLine("No transactions found for this account.");
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }
    }
}
