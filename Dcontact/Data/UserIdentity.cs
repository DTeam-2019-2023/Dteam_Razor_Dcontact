using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Dcontact.Data
{
    public class UserIdentity: IdentityUser
    {
        public bool isBan { get; set; }
        public UserIdentity()
        {
            TbOrderInformations = new HashSet<TbOrderInformation>();
            TbReports = new HashSet<TbReport>();
        }
        public virtual ICollection<TbOrderInformation> TbOrderInformations { get; set; }
        public virtual ICollection<TbReport> TbReports { get; set; }
    }
}
