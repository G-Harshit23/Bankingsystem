using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bankingsystem.Models
{
    [Index(nameof(AccountNumber), IsUnique = true)]
    public class Accounts
    {
        [Key]
        public int AccountId { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string AccountHolderName { get; set; }

        [Required]
        public decimal Balance { get; private set; } = 0;

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount should be greater than 0.");
            }
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount should be greater than 0.");
            }
            if (Balance < amount)
            {
                throw new InvalidOperationException("Insufficient balance.");
            }
            Balance -= amount;
        }
    }
}
