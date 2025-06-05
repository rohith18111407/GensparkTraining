
using DoctorPatientAppointment.Contexts;
using DoctorPatientAppointment.Interfaces;
using DoctorPatientAppointment.Models;
using DoctorPatientAppointment.Repositories;
using Microsoft.EntityFrameworkCore;

// public class PatientRepoTest
// {
//     private ClinicContext _context;

//     [SetUp]
//     public void Setup()
//     {
//         var options = new DbContextOptionsBuilder<ClinicContext>()
//                         .UseInMemoryDatabase(Guid.NewGuid().ToString()) // New DB for each test
//                         .Options;
//         _context = new ClinicContext(options);
//     }

    // [Test]
    // public async Task AddPatientTest()
    // {
    //     var patient = new Patient
    //     {
    //         Name = "Patient Test",
    //         Age = 30,
    //         Phone = "1234567890",
    //         Email = "patient@test.com"
    //     };
    //     IRepository<int, Patient> repo = new PatinetRepository(_context);
    //     var result = await repo.Add(patient);
    //     Assert.That(result, Is.Not.Null);
    //     Assert.That(result.Id, Is.GreaterThan(0));
    // }

    // [Test]
    // public async Task GetPatientPassTest()
    // {
    //     var patient = new Patient
    //     {
    //         Name = "Patient Get",
    //         Age = 40,
    //         Phone = "0987654321",
    //         Email = "get@patient.com"
    //     };
    //     IRepository<int, Patient> repo = new PatinetRepository(_context);
    //     var added = await repo.Add(patient);
    //     var result = repo.Get(added.Id);
    //     Assert.That(result, Is.Not.Null);
    //     Assert.That(result.Id, Is.EqualTo(added.Id));
    // }

    // [Test]
    // public async Task GetPatientExceptionTest()
    // {
    //     IRepository<int, Patient> repo = new PatinetRepository(_context);
    //     await repo.Add(new Patient { Name = "Test", Age = 22, Email = "x@y.com", Phone = "123456" }); // optional setup
    //     Assert.ThrowsAsync<Exception>(async () => await repo.Get(999));
    // }


    // [Test]
    // public async Task UpdatePatientTest()
    // {
    //     var patient = new Patient
    //     {
    //         Name = "Old Name",
    //         Age = 22,
    //         Phone = "1231231234",
    //         Email = "update@patient.com"
    //     };
    //     IRepository<int, Patient> repo = new PatinetRepository(_context);
    //     var added = await repo.Add(patient);
    //     added.Name = "New Name";

    //     var result = await repo.Update(added.Id, added);

    //     Assert.That(result.Name, Is.EqualTo("New Name"));
    // }


    // [Test]
    // public async Task DeletePatientTest()
    // {
    //     var patient = new Patient
    //     {
    //         Name = "Delete Patient",
    //         Age = 33,
    //         Phone = "5551231234",
    //         Email = "delete@patient.com"
    //     };

    //     IRepository<int, Patient> repo = new PatinetRepository(_context);
    //     var added = await repo.Add(patient);

    //     var deleted = await repo.Delete(added.Id);
    //     Assert.That(deleted, Is.Not.Null);

    //     // Use async version of the assertion
    //     Assert.ThrowsAsync<Exception>(async () => await repo.Get(added.Id));
    // }


    // [TearDown]
    // public void TearDown()
    // {
    //     _context.Dispose();
    // }
// }
