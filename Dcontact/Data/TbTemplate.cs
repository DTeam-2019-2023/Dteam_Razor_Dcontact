using System;
using System.Collections.Generic;

namespace Dcontact.Data
{
    public partial class TbTemplate
    {
        public TbTemplate()
        {
            TbRowDesigns = new HashSet<TbRowDesign>();
        }

        public string Id { get; set; } = null!;
        public string IdAvatar { get; set; } = null!;
        public string IdBackGround { get; set; } = null!;
        public string IdDcontact { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool IsApply { get; set; }

        public virtual TbAvatar IdAvatarNavigation { get; set; } = null!;
        public virtual TbBackGround IdBackGroundNavigation { get; set; } = null!;
        public virtual TbDcontact IdDcontactNavigation { get; set; } = null!;
        public virtual ICollection<TbRowDesign> TbRowDesigns { get; set; }
    }
}
