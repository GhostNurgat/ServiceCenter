using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ServiceCenter.Models;

namespace ServiceCenter
{
    public class ServiceCenterContext : DbContext
    {
        public ServiceCenterContext()
            : base("DbConnection")
        {
        }

        public DbSet<StaffMember> StaffMembers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Position> Positions { get; set; }
    }
}
