using Bankingsystem.Data;

namespace Bankingsystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDbContext context = new AppDbContext();
            Bankingtransaction bankingtransaction = new Bankingtransaction(context);

            while (true)
            {
                try
                {
                    int n = 0;
                    Console.WriteLine("=============================");
                    Console.WriteLine("Banking System");
                    Console.WriteLine("=============================");
                    Console.WriteLine("1. Create a New Account\r\n2. Deposit Money\r\n3. Withdraw Money\r\n4. Check Account Balance\r\n5. Transaction details\r\n6. Exit\r\n");
                    n = Convert.ToInt32(Console.ReadLine());

                    switch (n)
                    {
                        case 1:
                            
                            Console.WriteLine("Create a New Account");
                            bool flag = false;
                            string HolderName;
                            do
                             {
                                
                                Console.Write("Enter Holder Name: ");
                                HolderName = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(HolderName))
                                {
                                    Console.WriteLine("Error: Name cannot be empty or null. Please enter again.");
                                    flag = true;
                                }
                                else if (HolderName.Any(char.IsDigit))
                                {
                                    Console.WriteLine("Error: Name cannot contain numbers. Please enter again.");
                                    flag = true;
                                }
                            } while (flag);

                            string AccountNumber;
                            do
                            {
                                Console.Write("Enter AccountNumber: ");
                                AccountNumber = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(AccountNumber))
                                {
                                    Console.WriteLine("Error: AccountNumber cannot be empty or null. Please enter again.");
                                }
                                else if (!(AccountNumber.Any(char.IsDigit)))
                                {
                                    Console.WriteLine("Error: AccountNumber cannot contain characters. Please enter again.");
                                }
                            } while (string.IsNullOrWhiteSpace(AccountNumber)|| !(AccountNumber.Any(char.IsDigit)));

                            decimal Balance;
                            do
                            {
                                Console.Write("Enter Initial Balance: ");
                                if (!decimal.TryParse(Console.ReadLine(), out Balance) || Balance <= 0)
                                {
                                    Console.WriteLine("Error: Balance should be a positive decimal value. Please enter again.");
                                }
                                else
                                {
                                    break;
                                }
                            } while (true);

                            bankingtransaction.CreateAccount(HolderName, AccountNumber, Balance);
                            break;

                        case 2:
                            do
                            {
                                Console.Write("Enter AccountNumber: ");
                                AccountNumber = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(AccountNumber))
                                {
                                    Console.WriteLine("Error: AccountNumber cannot be empty or null. Please enter again.");
                                }
                                else if (!(AccountNumber.Any(char.IsDigit)))
                                {
                                    Console.WriteLine("Error: AccountNumber cannot contain characters. Please enter again.");
                                }
                            } while (string.IsNullOrWhiteSpace(AccountNumber) || !(AccountNumber.Any(char.IsDigit)));

                            decimal Amount;
                            do
                            {
                                Console.Write("Enter Amount: ");
                                if (!decimal.TryParse(Console.ReadLine(), out Amount) || Amount <= 0)
                                {
                                    Console.WriteLine("Error: Amount should be a positive decimal value. Please enter again.");
                                }
                                else
                                {
                                    break;
                                }
                            } while (true);

                            bankingtransaction.DepositMoney(AccountNumber, Amount);
                            break;

                        case 3:
                            do
                            {
                                Console.Write("Enter AccountNumber: ");
                                AccountNumber = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(AccountNumber))
                                {
                                    Console.WriteLine("Error: AccountNumber cannot be empty or null. Please enter again.");
                                }
                                else if (!(AccountNumber.Any(char.IsDigit)))
                                {
                                    Console.WriteLine("Error: AccountNumber cannot contain characters. Please enter again.");
                                }
                            } while (string.IsNullOrWhiteSpace(AccountNumber) || !(AccountNumber.Any(char.IsDigit)));

                            do
                            {
                                Console.Write("Enter Amount: ");
                                if (!decimal.TryParse(Console.ReadLine(), out Amount) || Amount <= 0)
                                {
                                    Console.WriteLine("Error: Amount should be a positive decimal value. Please enter again.");
                                }
                                else
                                {
                                    break;
                                }
                            } while (true);

                            bankingtransaction.WithdrawMoney(AccountNumber, Amount);
                            break;

                        case 4:
                            do
                            {
                                Console.Write("Enter AccountNumber: ");
                                AccountNumber = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(AccountNumber))
                                {
                                    Console.WriteLine("Error: AccountNumber cannot be empty or null. Please enter again.");
                                }
                                else if (!(AccountNumber.Any(char.IsDigit)))
                                {
                                    Console.WriteLine("Error: AccountNumber cannot contain characters. Please enter again.");
                                }
                            } while (string.IsNullOrWhiteSpace(AccountNumber) || !(AccountNumber.Any(char.IsDigit)));
                            bankingtransaction.CheckAccountBalance(AccountNumber);
                            break;
                        case 5:
                            do
                            {
                                Console.Write("Enter AccountNumber: ");
                                AccountNumber = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(AccountNumber))
                                {
                                    Console.WriteLine("Error: AccountNumber cannot be empty or null. Please enter again.");
                                }
                                else if (!(AccountNumber.Any(char.IsDigit)))
                                {
                                    Console.WriteLine("Error: AccountNumber cannot contain characters. Please enter again.");
                                }
                            } while (string.IsNullOrWhiteSpace(AccountNumber) || !(AccountNumber.Any(char.IsDigit)));
                            bankingtransaction.GetTransactions(AccountNumber);
                            break;
                        case 6:
                            return;

                        default:
                            Console.WriteLine("Enter valid option");
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
