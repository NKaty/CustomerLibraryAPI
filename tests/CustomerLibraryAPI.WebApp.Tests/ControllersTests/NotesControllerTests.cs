using CustomerLibraryAPI.BusinessEntities;
using CustomerLibraryAPI.Repositories;
using CustomerLibraryAPI.WebApp.Controllers;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CustomerLibraryAPI.WebApp.Tests.ControllersTests
{
    public class NotesControllerTests
    {
        [Fact]
        public void ShouldGetNoteesByCustomerId()
        {
            var noteRepositoryMock = new Mock<IDependentRepository<Note>>();
            var note = new Note { CustomerId = 1 };
            noteRepositoryMock.Setup(r => r.ReadByCustomerId(1)).Returns(new List<Note> { note });

            var controller = new NotesController(noteRepositoryMock.Object);
            var notees = controller.GetByCustomerId(1);

            Assert.NotNull(notees);
            Assert.Single(notees);
        }

        [Fact]
        public void ShouldGetNote()
        {
            var noteRepositoryMock = new Mock<IDependentRepository<Note>>();
            var note = new Note { NoteId = 1 };
            noteRepositoryMock.Setup(r => r.Read(1)).Returns(note);

            var controller = new NotesController(noteRepositoryMock.Object);
            var data = controller.Get(1);

            Assert.NotNull(data);
            Assert.Equal(1, data.NoteId);
        }

        [Fact]
        public void ShouldCreateNote()
        {
            var noteRepositoryMock = new Mock<IDependentRepository<Note>>();
            var note = new Note();
            noteRepositoryMock.Setup(r => r.Create(note)).Returns(1);


            var controller = new NotesController(noteRepositoryMock.Object);
            controller.Post(note);

            noteRepositoryMock.Verify(r => r.Create(note), Times.Exactly(1));
        }

        [Fact]
        public void ShouldUpdateNote()
        {
            var noteRepositoryMock = new Mock<IDependentRepository<Note>>();
            var note = new Note { NoteId = 1 };
            noteRepositoryMock.Setup(r => r.Update(note));


            var controller = new NotesController(noteRepositoryMock.Object);
            controller.Put(note);

            noteRepositoryMock.Verify(r => r.Update(note), Times.Exactly(1));
        }

        [Fact]
        public void ShouldDeleteNote()
        {
            var noteRepositoryMock = new Mock<IDependentRepository<Note>>();
            var note = new Note { NoteId = 1 };
            noteRepositoryMock.Setup(r => r.Delete(1));


            var controller = new NotesController(noteRepositoryMock.Object);
            controller.Delete(1);

            noteRepositoryMock.Verify(r => r.Delete(1), Times.Exactly(1));
        }
    }
}
