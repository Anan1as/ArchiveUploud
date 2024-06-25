using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ArchiveUploud.Models
{
    [Table("Folders")]
    public class Folder
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Folder needs a name.")]
        [Column("Name")]
        public string? Name { get; set; }

        [Column("ParentFolderId")]
        public int? ParentFolderId { get; set; }

        [JsonIgnore]
        public Folder? ParentFolder { get; set; }

        public ICollection<Folder>? SubFolders { get; set; } = new List<Folder>();

        public ICollection<FileRecord>? Files { get; set; } = new List<FileRecord>();
    }
}