using CustomerLibraryAPI.BusinessEntities;
using CustomerLibraryAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CustomerLibraryAPI.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IDependentRepository<Note> _noteRepository;

        public NotesController(IDependentRepository<Note> noteRepository)
        {
            _noteRepository = noteRepository;
        }

        // GET: api/<NotesController>?customerId=10
        [HttpGet]
        public IEnumerable<Note> GetByCustomerId(int customerId)
        {
            return _noteRepository.ReadByCustomerId(customerId);
        }

        // GET api/<NotesController>/5
        [HttpGet("{noteId}")]
        public Note Get(int noteId)
        {
            return _noteRepository.Read(noteId);
        }

        // POST api/<NotesController>
        [HttpPost]
        public void Post([FromBody] Note note)
        {
            _noteRepository.Create(note);
        }

        // PUT api/<NotesController>/5
        [HttpPut("{noteId}")]
        public void Put([FromBody] Note note)
        {
            _noteRepository.Update(note);
        }

        // DELETE api/<NotesController>/5
        [HttpDelete("{noteId}")]
        public void Delete(int noteId)
        {
            _noteRepository.Delete(noteId);
        }
    }
}
