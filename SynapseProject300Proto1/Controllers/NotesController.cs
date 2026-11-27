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
            return CreatedAtAction(nameof(GetNoteById), new { id = note.NoteId }, note);
        }

        [HttpGet("{id}")] //gets by id
        public async Task<ActionResult<Note>> GetNoteById(long id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
                //returns a 404 (not found)
            }
            return Ok(note); //returns a 200 (success) and the note
        }

        [HttpPut("{id}")] //updates by id
        public async Task<IActionResult> UpdateNote(long id, Note updatedNote)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound(); //returns a 404 (not found)
            }
            note.Title = updatedNote.Title;
            note.Content = updatedNote.Content;
            note.Keys = updatedNote.Keys;
            await _context.SaveChangesAsync();
            return NoContent(); //returns a 204 (success) with no content
        }

        [HttpDelete("{id}")] //deletes by id
        public async Task<IActionResult> DeleteNote(long id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound(); //returns a 404 (not found)
            }
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return NoContent(); //returns a 204 (success) with no content
        }
        #endregion
    }
}