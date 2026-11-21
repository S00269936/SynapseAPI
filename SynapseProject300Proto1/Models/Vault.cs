namespace SynapseProject300Proto1.Models
{
    public class Vault
    {
        public int VaultId { get; set; }
        public int OwnerUserId { get; set; }
        public required string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
