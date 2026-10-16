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

        [HttpPost] //posts
        public async Task<ActionResult<Note>> CreateNote(Note note)
        {
            _context.Notes.Add(note); 
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
            //returns a 201 (success) and location of note
        }

        [HttpGet("{id}")] //gets by id
        public async Task<ActionResult<Note>> GetNoteById(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound(); 
                //returns a 404 (not found)
            }
            return Ok(note); //returns a 200 (success) and the note
        }
        #endregion
    }
}