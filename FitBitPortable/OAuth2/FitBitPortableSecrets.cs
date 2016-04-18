using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitBitPortable.OAuth2
{
    public class Secrets
    {
        public Secrets(string ClientID, string ClientSecret) { this.ClientId = ClientId; this.ClientSecret = ClientSecret; }

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
