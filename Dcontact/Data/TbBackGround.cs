using System;
using System.Collections.Generic;

namespace Dcontact.Data
{
    public partial class TbBackGround
    {
        public TbBackGround()
        {
            TbTemplates = new HashSet<TbTemplate>();
        }

        public string Id { get; set; } = null!;
        public string PictureLocation { get; set; } = null!;

        public virtual ICollection<TbTemplate> TbTemplates { get; set; }
    }
}
