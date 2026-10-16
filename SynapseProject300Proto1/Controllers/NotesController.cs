using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SynapseProject300Proto1.Controllers.Model;
using SynapseProject300Proto1.Models;

namespace SynapseProject300Proto1.Controllers
{
    [Route("api/[controller]")] //url for api - https://localhost:PORT/api/notes
    [ApiController] //this class handles api requests and responses (notes)
    public class NotesController : ControllerBase
    {
        private readonly SynapseDbContext _context; //adds reference to db context
        public NotesController(SynapseDbContext context)
        {
            _context = context;
            //injects db context into controller class
        }

        #region Notes CRUD Operations
        [HttpGet] //responds to GET requests
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            return await _context.Notes.ToListAsync();
            //_context.Notes is a DbSet<Note> representing the Notes table in the database
        }

        #endregion
    }
}