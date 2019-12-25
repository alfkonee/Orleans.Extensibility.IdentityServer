using System.Collections.Generic;

namespace Orleans.Extensibility.IdentityServer.Grains
{
    public class OrleansIdentityResource
    {
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public bool Emphasize { get; set; } = false;

        public bool Enable { get; set; } = true;
        public bool ShowInDiscoveryDocument  { get; set; } = true;
        public string Name { get; set; }
        public bool Required { get; set; } = false;
        public IDictionary<string,string> Properties { get; set; }
        public ICollection<string> UserClaims { get; set; }
    }
    
}