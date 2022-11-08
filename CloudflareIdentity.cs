using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroLake.Cloudflare.Identity
{
    internal class CloudflareIdentity : ICloudflareIdentity
    {
        public string Email { get; set; }
        public string Issuer { get; set; }
        public string Country { get; set; }
    }
}
