using System;
using System.Collections.Generic;

namespace Dcontact.Data
{
    public partial class TbRowDesign
    {
        public string Id { get; set; } = null!;
        public string IdRowContent { get; set; } = null!;
        public string IdTemplate { get; set; } = null!;
        public string Font { get; set; } = null!;
        public string RowColor { get; set; } = null!;
        public string Bullet { get; set; } = null!;

        public virtual TbTemplate IdTemplateNavigation { get; set; } = null!;
        public virtual TbRowContent IdRowContentNavigation { get; set; } = null!;

    }
}
