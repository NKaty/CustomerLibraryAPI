using CustomerLibraryAPI.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CustomerLibraryAPI.BusinessEntities;
using CustomerLibraryAPI.Common;

namespace CustomerLibraryAPI.Data.EFRepositories
{
    public class NoteRepository : IDependentRepository<Note>
    {
        private readonly CustomerLibraryContext _context;

        public NoteRepository()
        {
            _context = CustomerLibraryContextProvider.Current;
        }

        public NoteRepository(CustomerLibraryContext context)
        {
            _context = context;
        }

        public int Create(Note note)
        {
            var newNote = _context.Notes.Add(note).Entity;

            _context.SaveChanges();

            return newNote.NoteId;
        }

        public Note Read(int noteId)
        {
            var note = _context.Notes.Find(noteId);

            if (note is null)
            {
                throw new NotFoundException("Note is not found.");
            }

            return note;
        }

        public List<Note> ReadByCustomerId(int customerId)
        {
            return _context.Notes.Where(n => n.CustomerId == customerId).ToList();
        }

        public int CountByCustomerId(int customerId)
        {
            return _context.Notes.Count(a => a.CustomerId == customerId);
        }

        public void Update(Note note)
        {
            var dbNote = _context.Notes.Find(note.NoteId);

            if (dbNote is null)
            {
                throw new NotFoundException("Note is not found.");
            }

            _context.Entry(dbNote).CurrentValues.SetValues(note);

            _context.SaveChanges();
        }

        public void Delete(int noteId)
        {
            var note = _context.Notes.Find(noteId);

            if (note is null)
            {
                throw new NotFoundException("Note is not found.");
            }

            var count = CountByCustomerId(note.CustomerId);

            if (count < 2)
            {
                throw new NotDeletedException("Cannot delete the only note.");
            }

            _context.Notes.Remove(note);

            _context.SaveChanges();
        }

        public void DeleteAll()
        {
            var notes = _context.Notes.ToList();

            foreach (var note in notes)
            {
                _context.Notes.Remove(note);
            }

            _context.SaveChanges();
        }
    }
}