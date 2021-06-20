using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Models
{
    public class Position
    {
        public int PositionId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<StaffMember> StaffMembers { get; set; }
        public Position()
        {
            StaffMembers = new List<StaffMember>();
        }
    }
}
