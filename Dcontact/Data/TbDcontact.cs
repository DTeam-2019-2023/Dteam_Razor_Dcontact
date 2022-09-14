using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace Dcontact.Data
{
    public partial class TbDcontact
    {
        public TbDcontact()
        {
            TbTemplates = new HashSet<TbTemplate>();
        }

        public string Id { get; set; } = null!;
        public string IdUser { get; set; } = null!;
        public virtual UserIdentity IdUserIdentityNavigation { get; set; } = null!;
        public virtual ICollection<TbTemplate> TbTemplates { get; set; }

        
    }
}
