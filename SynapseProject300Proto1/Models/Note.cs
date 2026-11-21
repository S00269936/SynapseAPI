namespace SynapseProject300Proto1.Models
{
    public class Note
    {
        //Same table data as Corhy's SQL tables
        public long NoteId { get; set; }
        public int VaultId { get; set; }
        public int? FolderId { get; set; }
        public required string Title { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public string? Keys { get; set; }
    }
}
