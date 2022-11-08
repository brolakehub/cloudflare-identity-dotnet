using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroLake.Cloudflare.Identity
{
    public class NotAuthenticatedException : Exception
    {
        public NotAuthenticatedException() : base($"This user is not authenticated") { }
    }
}
