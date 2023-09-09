using System;
using System.Collections.Generic;

namespace Dcontact.Data
{
    public partial class TbRowContent
    {
        public TbRowContent()
        {
            TbReports = new HashSet<TbReport>();
            TbRowDesigns = new HashSet<TbRowDesign>();
        }

        public string Id { get; set; } = null!;
        public string IdDcontact { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string Link { get; set; } = null!;
        public string? Code { get; set; }
        public DateTime? Birth { get; set; }
        public int Click { get; set; }

        public virtual TbDcontact IdDcontactNavigation { get; set; } = null!;
        public virtual ICollection<TbReport> TbReports { get; set; }
        public virtual ICollection<TbRowDesign> TbRowDesigns { get; set; }
    }
}

