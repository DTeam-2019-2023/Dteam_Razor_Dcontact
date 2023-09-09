using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace Dcontact.Data
{
    public partial class TbDcontact
    {
        public TbDcontact()
        {
            TbRowContents = new HashSet<TbRowContent>();
            TbTemplates = new HashSet<TbTemplate>();
        }

        public string Id { get; set; } = null!;
        public string IdUser { get; set; } = null!;
        public int view { get; set; } = default!;

        public virtual UserIdentity IdUserNavigation { get; set; } = null!;
        public virtual ICollection<TbRowContent> TbRowContents { get; set; }
        public virtual ICollection<TbTemplate> TbTemplates { get; set; }

    }
}
