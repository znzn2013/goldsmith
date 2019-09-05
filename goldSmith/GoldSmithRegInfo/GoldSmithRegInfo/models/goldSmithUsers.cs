using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GoldSmithRegInfo.models
{
    public class goldSmithUsers : IdentityUser
    {
        [Column(TypeName = "nvarchar(20)")]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string LastName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string ShopName { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string LicensNumber { get; set; }
        public bool Active { get;set; }

    }
}
