using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArchiveUploud.Data;
using ArchiveUploud.Models;
using System.Threading.Tasks;

namespace ArchiveUploud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly UplodingDBContext _context;

        public FilesController(UplodingDBContext context)
        {
            _context = context;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file, int folderId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is null or empty.");
            }

            var folder = await _context.Folders.FindAsync(folderId);
            if (folder == null)
            {
                return NotFound("Folder not found.");
            }

            var filePath = Path.Combine("Files", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileRecord = new FileRecord
            {
                FileName = file.FileName,
                FilePath = filePath,
                FolderId = folderId
            };

            _context.Files.Add(fileRecord);
            await _context.SaveChangesAsync();

            return Ok(new { fileRecord.Id, fileRecord.FileName, fileRecord.FilePath });
        }

        [HttpPost("create-folder")]
        public async Task<IActionResult> CreateFolder([FromBody] FolderCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Folder parentFolder = null;
            if (model.ParentFolderId.HasValue)
            {
            parentFolder = await _context.Folders.FindAsync(model.ParentFolderId.Value);
                if (parentFolder == null)
                {
                    return NotFound("Parent folder not found.");
                }
            }

            var folder = new Folder 
            { 
                Name = model.Name,
                ParentFolderId = model.ParentFolderId
            };

            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();
            return Ok(folder);
        }

        /*[HttpPost("create-folder")]
        public async Task<IActionResult> CreateFolder([FromBody] FolderCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var folder = new Folder { Name = model.Name };

            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();
            return Ok(folder);
        }*/

        [HttpGet("folders")]
        public async Task<IActionResult> GetFolders()
        {
            var folders = await _context.Folders.Include(f => f.Files).ToListAsync();
            return Ok(folders);
        }
    }
}