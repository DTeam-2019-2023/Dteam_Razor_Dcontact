using System;
using System.Collections.Generic;

namespace Dcontact.Data
{
    public partial class TbReport
    {
        public string Id { get; set; } = null!;
        public string IdRow { get; set; } = null!;
        public string IdUser { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Status { get; set; }

        public virtual TbRowContent IdRowNavigation { get; set; } = null!;

        public virtual UserIdentity IdUserIdentityNavigation { get; set; } = null!;

    }
}
