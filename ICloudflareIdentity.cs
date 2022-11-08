namespace BroLake.Cloudflare.Identity;

public interface ICloudflareIdentity
{
    string Email { get; set; }
    string Issuer { get; set; }
    string Country { get; set; }
}