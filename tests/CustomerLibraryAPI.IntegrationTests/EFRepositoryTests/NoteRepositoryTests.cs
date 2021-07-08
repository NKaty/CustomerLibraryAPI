﻿using CustomerLibraryAPI.BusinessEntities;
using CustomerLibraryAPI.Common;
using CustomerLibraryAPI.Data.EFRepositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CustomerLibraryAPI.IntegrationTests.EFRepositoryTests
{
    public class NoteRepositoryTests
    {
        [Fact]
        public void ShouldBeAbleToCreateNoteRepository()
        {
            var noteRepository = new NoteRepository();
            Assert.NotNull(noteRepository);
        }

        [Fact]
        public void ShouldBeAbleToCreateNoteRepositoryWithContext()
        {
            var context = new CustomerLibraryContext(new DbContextOptions<CustomerLibraryContext>());
            var noteRepository = new NoteRepository(context);
            Assert.NotNull(noteRepository);
        }

        [Fact]
        public void ShouldBeAbleToCreateNote()
        {
            var fixture = new NoteRepositoryFixture();
            var mockNoteId = fixture.CreateMockNote();
            Assert.NotEqual(0, mockNoteId);
        }

        [Fact]
        public void ShouldBeAbleToReadNote()
        {
            var noteRepository = new NoteRepository();
            var fixture = new NoteRepositoryFixture();
            var noteId = fixture.CreateMockNote();
            var createdNote = noteRepository.Read(noteId);

            Assert.NotNull(createdNote);
            Assert.Equal(fixture.MockNote.NoteId, createdNote.NoteId);
            Assert.Equal(fixture.MockNote.CustomerId, createdNote.CustomerId);
            Assert.Equal(fixture.MockNote.NoteText, createdNote.NoteText);
        }

        [Fact]
        public void ShouldThrowNotFoundExceptionWhileReadingNote()
        {
            var noteRepository = new NoteRepository();
            var fixture = new NoteRepositoryFixture();
            fixture.CreateMockNote();

            Assert.Throws<NotFoundException>(() => noteRepository.Read(0));
        }

        [Fact]
        public void ShouldBeAbleToReadNotesByCustomerId()
        {
            var noteRepository = new NoteRepository();
            var fixture = new NoteRepositoryFixture();
            var customerId = fixture.CreateMockNotes();
            var createdNotes = noteRepository.ReadByCustomerId(customerId);

            Assert.Equal(2, createdNotes.Count);
            Assert.Equal(customerId, createdNotes[0].CustomerId);
            Assert.Equal(customerId, createdNotes[1].CustomerId);
        }

        [Fact]
        public void ShouldBeAbleToCountNotesByCustomerId()
        {
            var noteRepository = new NoteRepository();
            var fixture = new NoteRepositoryFixture();
            var customerId = fixture.CreateMockNotes();
            var count = noteRepository.CountByCustomerId(customerId);

            Assert.Equal(2, count);
        }

        [Fact]
        public void ShouldBeAbleToUpdateNote()
        {
            var noteRepository = new NoteRepository();
            var fixture = new NoteRepositoryFixture();
            var noteId = fixture.CreateMockNote();

            fixture.MockNote.NoteText = "Test";
            noteRepository.Update(fixture.MockNote);
            var updatedNote = noteRepository.Read(noteId);

            Assert.NotNull(updatedNote);
            Assert.Equal(fixture.MockNote.NoteId, updatedNote.NoteId);
            Assert.Equal(fixture.MockNote.CustomerId, updatedNote.CustomerId);
            Assert.Equal("Test", updatedNote.NoteText);
        }

        [Fact]
        public void ShouldThrowNotFoundExceptionWhileUpdatingNote()
        {
            var noteRepository = new NoteRepository();
            var fixture = new NoteRepositoryFixture();
            fixture.CreateMockNote();

            Assert.Throws<NotFoundException>(() => noteRepository.Update(new Note { NoteId = 0 }));
        }

        [Fact]
        public void ShouldBeAbleToDeleteNote()
        {
            var noteRepository = new NoteRepository();
            var fixture = new NoteRepositoryFixture();
            var noteId = fixture.CreateMockNote();
            var createdNote = noteRepository.Read(noteId);

            Assert.NotNull(createdNote);

            noteRepository.Delete(noteId);

            Assert.Throws<NotFoundException>(() => noteRepository.Read(noteId));
        }

        [Fact]
        public void ShouldThrowNotFoundExceptionWhileDeletingNote()
        {
            var noteRepository = new NoteRepository();
            var fixture = new NoteRepositoryFixture();
            fixture.CreateMockNote();

            Assert.Throws<NotFoundException>(() => noteRepository.Delete(0));
        }
    }

    public class NoteRepositoryFixture
    {
        public Note MockNote { get; set; } = new Note
        {
            NoteText = "Note1"
        };

        public Note MockNote2 { get; set; } = new Note
        {
            NoteText = "Note1"
        };

        public int CreateMockNote(int customerId = 0)
        {
            var noteRepository = new NoteRepository();

            if (customerId == 0)
            {
                var customerFixture = new CustomerRepositoryFixture();
                customerId = customerFixture.CreateMockCustomer();
            }

            noteRepository.DeleteAll();

            MockNote.CustomerId = customerId;
            var newNoteId = noteRepository.Create(MockNote);
            return newNoteId;
        }

        public int CreateMockNotes()
        {
            var noteRepository = new NoteRepository();

            var customerFixture = new CustomerRepositoryFixture();
            var customerId = customerFixture.CreateMockCustomer();

            noteRepository.DeleteAll();

            MockNote.CustomerId = customerId;
            MockNote2.CustomerId = customerId;
            noteRepository.Create(MockNote);
            noteRepository.Create(MockNote2);
            return customerId;
        }
    }
}