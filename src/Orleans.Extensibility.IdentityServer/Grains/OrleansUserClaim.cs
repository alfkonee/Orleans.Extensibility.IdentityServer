namespace Orleans.Extensibility.IdentityServer.Grains
{
    public class OrleansUserClaim
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}