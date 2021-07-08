using CustomerLibraryAPI.BusinessEntities;
using CustomerLibraryAPI.Data.EFRepositories;
using CustomerLibraryAPI.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CustomerLibraryAPI.WebApp.Tests
{
    public class ServiceCollectionTests
    {
        [Fact]
        public void ShouldRegisterServices()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IDependentRepository<Note>, NoteRepository>();
            serviceCollection.AddSingleton<IDependentRepository<Address>, AddressRepository>();
            serviceCollection.AddScoped<IMainRepository<Customer>, CustomerRepository>();
            serviceCollection.AddTransient<TestDI>();

            var provider = serviceCollection.BuildServiceProvider();

            var noteRepository = provider.GetService<IDependentRepository<Note>>();
            Assert.IsAssignableFrom<IDependentRepository<Note>>(noteRepository);

            var testDI = provider.GetService<TestDI>();
            Assert.IsAssignableFrom<IDependentRepository<Note>>(testDI.NoteRepository);
            Assert.NotSame(noteRepository, testDI.NoteRepository);
        }

        [Fact]
        public void ShouldRegisterServicesAsSingleton()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IDependentRepository<Note>, NoteRepository>();
            serviceCollection.AddSingleton<IDependentRepository<Address>, AddressRepository>();
            serviceCollection.AddScoped<IMainRepository<Customer>, CustomerRepository>();
            serviceCollection.AddTransient<TestDI>();

            var provider = serviceCollection.BuildServiceProvider();

            var addressRepository = provider.GetService<IDependentRepository<Address>>();
            Assert.IsAssignableFrom<IDependentRepository<Address>>(addressRepository);

            var testDI = provider.GetService<TestDI>();
            Assert.IsAssignableFrom<IDependentRepository<Address>>(testDI.AddressRepository);
            Assert.Same(addressRepository, testDI.AddressRepository);
        }

        [Fact]
        public void ShouldRegisterServicesAsScoped()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IDependentRepository<Note>, NoteRepository>();
            serviceCollection.AddSingleton<IDependentRepository<Address>, AddressRepository>();
            serviceCollection.AddScoped<IMainRepository<Customer>, CustomerRepository>();
            serviceCollection.AddTransient<TestDI>();

            var provider = serviceCollection.BuildServiceProvider();

            var customerRepository = provider.GetService<IMainRepository<Customer>>();
            Assert.IsAssignableFrom<IMainRepository<Customer>>(customerRepository);

            var testDI = provider.GetService<TestDI>();
            Assert.IsAssignableFrom<IMainRepository<Customer>>(testDI.CustomerRepository);
            Assert.Same(customerRepository, testDI.CustomerRepository);
        }

        public class TestDI
        {
            public IDependentRepository<Note> NoteRepository { get; }

            public IMainRepository<Customer> CustomerRepository { get; }

            public IDependentRepository<Address> AddressRepository { get; }

            public TestDI(IDependentRepository<Note> noteRepository, IDependentRepository<Address> addressRepository,
                IMainRepository<Customer> customerRepository)
            {
                NoteRepository = noteRepository;
                AddressRepository = addressRepository;
                CustomerRepository = customerRepository;
            }
        }
    }
}