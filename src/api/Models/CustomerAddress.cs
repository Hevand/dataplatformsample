using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public partial class CustomerAddress
    {
        [Key]
        public int CustomerId { get; set; }
        [Key]
        public int AddressId { get; set; }
        public string AddressType { get; set; } = null!;
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
    }
}
