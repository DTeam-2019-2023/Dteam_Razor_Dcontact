using System;
using System.Collections.Generic;

namespace Dcontact.Data
{
    public partial class TbOrderInformation
    {
        public string TradingCode { get; set; } = null!;
        public string IdUser { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public decimal ExportPrice { get; set; }
        public string PitureLocation { get; set; } = null!;

        public virtual UserIdentity IdUserIdentityNavigation { get; set; } = null!;

    }
}
