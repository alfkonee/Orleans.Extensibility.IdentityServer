using System.Collections.Generic;

namespace Orleans.Extensibility.IdentityServer.Grains
{
    public class OrleansApiResource
    {
        public ICollection<OrleansSecret> ApiSecrets { get; set; }
        public ICollection<string> UserClaims { get; set; }
        public ICollection<OrleansScope> Scopes { get; set; }
    }
}