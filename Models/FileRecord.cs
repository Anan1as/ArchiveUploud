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
    [Table("Files")]
    public class FileRecord
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "File needs a name.")]
        [Column("FileName")]
        public string? FileName { get; set; }

        [Required(ErrorMessage = "File needs a path.")]
        [Column("FilePath")]
        public string? FilePath { get; set; }

        [Required(ErrorMessage = "File needs a folder.")]
        [Column("FolderId")]
        public int FolderId { get; set; }

        [JsonIgnore]
        public Folder? Folder { get; set; }
    }
}