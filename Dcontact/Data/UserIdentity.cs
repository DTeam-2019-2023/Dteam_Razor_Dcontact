using Microsoft.AspNetCore.Identity;

namespace Dcontact.Data
{
    public class UserIdentity: IdentityUser
    {
        public UserIdentity()
        {
            TbOrderInformations = new HashSet<TbOrderInformation>();
        }
        public virtual ICollection<TbOrderInformation> TbOrderInformations { get; set; }
    }
}
