using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        /// <summary>
        /// IsAdmin can be set only in the Db manually for improved security
        /// </summary>
        public bool IsAdmin { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public ICollection<Ticket>? Tickets { get; set; }
    }
}
