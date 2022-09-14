using System;
using System.Collections.Generic;

namespace Dcontact.Data
{
    public partial class TbAvatar
    {
        public TbAvatar()
        {
            TbTemplates = new HashSet<TbTemplate>();
        }

        public string Id { get; set; } = null!;
        public string PictureLocation { get; set; } = null!;

        public virtual ICollection<TbTemplate> TbTemplates { get; set; }
    }
}
