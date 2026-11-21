using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SynapseProject300Proto1.Controllers.Model;
using SynapseProject300Proto1.Models;

namespace SynapseProject300Proto1.Controllers
{
    [Route("api/[controller]")] //url for api - https://localhost:PORT/api/folders
    [ApiController] //this class handles api requests and responses (folders)
    public class FoldersController : ControllerBase
    {
        private readonly SynapseDbContext _context; //adds reference to db context
        public FoldersController(SynapseDbContext context)
        {
            _context = context;
            //injects db context into controller class
        }

        #region Folders CRUD Operations
        [HttpGet] //responds to GET requests
        public async Task<ActionResult<IEnumerable<Folder>>> GetFolders()
        {
            return await _context.Folders.ToListAsync();
            //_context.Folders is a DbSet<Folder> representing the Folder table in the database
        }

        [HttpPost] //posts
        public async Task<ActionResult<Folder>> CreateFolder(Folder folder)
        {
            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFolderById), new { id = folder.FolderId }, folder);
        }

        [HttpGet("{id}")] //gets by id
        public async Task<ActionResult<Folder>> GetFolderById(int id)
        {
            var folder = await _context.Folders.FindAsync(id);
            if (folder == null)
            {
                return NotFound();
                //returns a 404 (not found)
            }
            return Ok(folder); //returns a 200 (success) and the folder
        }

        [HttpPut("{id}")] //updates by id
        public async Task<IActionResult> UpdateFolder(int id, Folder updatedFolder)
        {
            var folder = await _context.Folders.FindAsync(id);
            if (folder == null)
            {
                return NotFound(); //returns a 404 (not found)
            }
            folder.Name = updatedFolder.Name;
            folder.ParentFolderId = updatedFolder.ParentFolderId;
            await _context.SaveChangesAsync();
            return NoContent(); //returns a 204 (success) with no content
        }

        [HttpDelete("{id}")] //deletes by id
        public async Task<IActionResult> DeleteFolder(int id)
        {
            var folder = await _context.Folders.FindAsync(id);
            if (folder == null)
            {
                return NotFound(); //returns a 404 (not found)
            }
            _context.Folders.Remove(folder);
            await _context.SaveChangesAsync();
            return NoContent(); //returns a 204 (success) with no content
        }
        #endregion
    }
}