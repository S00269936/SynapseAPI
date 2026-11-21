namespace SynapseProject300Proto1.Models
{
    public class Folder
    {
        public int FolderId { get; set; }
        public int VaultId { get; set; }
        public int? ParentFolderId { get; set; }
        public required string Name { get; set; }
    }
}
