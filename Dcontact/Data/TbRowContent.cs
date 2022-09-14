using System;
using System.Collections.Generic;

namespace Dcontact.Data
{
    public partial class TbRowContent
    {
        public string Id { get; set; } = null!;
        public string IdRowDesign { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string Link { get; set; } = null!;
        public string Code { get; set; } = null!;
        public DateTime Birth { get; set; }
        public int Click { get; set; }

        public virtual TbRowDesign IdRowDesignNavigation { get; set; } = null!;
    }
}
