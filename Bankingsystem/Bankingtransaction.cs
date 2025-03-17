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
    }
}
