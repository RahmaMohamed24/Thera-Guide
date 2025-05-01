using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheraGuide.Entity;
using TheraGuide.Repository;
using TheraGuide.ViewModels;

namespace TheraGuide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IGenericRepository<Notes> _notesRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotesController(
            IGenericRepository<Notes> notesRepository,
            UserManager<ApplicationUser> userManager)
        {
            _notesRepository = notesRepository;
            _userManager = userManager;
        }
        // GET: api/notes/my
        [HttpGet("list")]
        public async Task<IActionResult> GetMyNotes()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var notes = await _notesRepository.FindAsync(n => n.UserId == userId);
            return Ok(notes);
        }

        // GET api/notes/5

        [Route("get/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetNote(long id)
        {
            var note = await _notesRepository.GetByIdAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            // Verify user owns the note
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (note.UserId != userId)
            {
                return Forbid();
            }

            return Ok(note);
        }

        // POST api/notes
        [Route("add")]
        [HttpPost]

        public async Task<IActionResult> CreateNote( NoteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            var note = new Notes
            {
                Title = model.Title,
                Note = model.Content,
                Date = model.Date ?? DateTime.UtcNow,
                CreationDate = DateTime.UtcNow,
                UserId = userId,
                User = user
                
            };

            var createdNote = await _notesRepository.InsertAsync(note);
            await _notesRepository.SaveAsync();
            return CreatedAtAction(nameof(GetNote), new { id = createdNote.Id }, createdNote);
        }

        // PUT api/notes/5
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateNote(int id, NoteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingNote = await _notesRepository.GetByIdAsync(id);
            if (existingNote == null)
            {
                return NotFound();
            }

            // Verify user owns the note
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (existingNote.UserId != userId)
            {
                return Forbid();
            }

            existingNote.Title = model.Title ?? existingNote.Title;
            existingNote.Note = model.Content ?? existingNote.Note;
            existingNote.Date = model.Date ?? existingNote.Date;
            existingNote.CreationDate = DateTime.UtcNow;

            var updatedNote = await _notesRepository.UpdateAsync(existingNote);
            await _notesRepository.SaveAsync();
            return Ok(updatedNote);
        }

        // DELETE api/notes/5

        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteNote(long id)
        {
            var note = await _notesRepository.GetByIdAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            // Verify user owns the note
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (note.UserId != userId)
            {
                return Forbid();
            }

            await _notesRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}

