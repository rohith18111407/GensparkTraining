
// using DoctorPatientAppointment.Contexts;
// using DoctorPatientAppointment.Interfaces;
// using DoctorPatientAppointment.Models;
// using DoctorPatientAppointment.Repositories;
// using Microsoft.EntityFrameworkCore;

// namespace DoctorPatientAppointment.Test;

// public class Tests
// {
//     private ClinicContext _context;
//     [SetUp]
//     public void Setup()
//     {
//         var options = new DbContextOptionsBuilder<ClinicContext>()
//                             .UseInMemoryDatabase("TestDb")
//                             .Options;
//         _context = new ClinicContext(options);
//     }

//     // [Test]
//     // public async Task AddDoctorTest()
//     // {
//     //     //arrange
//     //     var email = " test@gmail.com";
//     //     var password = System.Text.Encoding.UTF8.GetBytes("test123");
//     //     var key = Guid.NewGuid().ToByteArray();
//     //     var user = new User
//     //     {
//     //         Username = email,
//     //         Password = password,
//     //         HashKey = key,
//     //         Role = "Doctor"
//     //     };
//     //     _context.Add(user);
//     //     await _context.SaveChangesAsync();
//     //     var doctor = new Doctor
//     //     {
//     //         Name = "test",
//     //         YearsOfExperience = 2,
//     //         Email = email
//     //     };
//     //     IRepository<int, Doctor> _doctorRepository = new DoctorRepository(_context);
//     //     //action
//     //     var result = await _doctorRepository.Add(doctor);
//     //     //assert
//     //     Assert.That(result, Is.Not.Null, "Doctor IS not addeed");
//     //     // Assert.That(result.Id, Is.EqualTo(2));
//     //     Assert.That(result.Id, Is.EqualTo(1));
//     // }




//     // [TestCase(1)]
//     // [TestCase(2)]
//     // public async Task GetDoctorPassTest(int id)
//     // {
//     //     //arrange
//     //     var email = " test@gmail.com"
//     // ;
//     //     var password = System.Text.Encoding.UTF8.GetBytes("test123");
//     //     var key = Guid.NewGuid().ToByteArray();
//     //     var user = new User
//     //     {
//     //         Username = email,
//     //         Password = password,
//     //         HashKey = key,
//     //         Role = "Doctor"
//     //     };
//     //     _context.Add(user);
//     //     await _context.SaveChangesAsync();
//     //     var doctor = new Doctor
//     //     {
//     //         Name = "test",
//     //         YearsOfExperience = 2,
//     //         Email = email
//     //     };
//     //     IRepository<int, Doctor> _doctorRepository = new DoctorRepository(_context);
//     //     //action
//     //     await _doctorRepository.Add(doctor);

//     //     //action
//     //     var result = _doctorRepository.Get(id);
//     //     //assert
//     //     Assert.That(result.Id, Is.EqualTo(id));

//     // }



//     [TestCase(3)]
//     public async Task GetDoctorExceptionTest(int id)
//     {
//         //arrange
//         var email = " test@gmail.com";
//         var password = System.Text.Encoding.UTF8.GetBytes("test123");
//         var key = Guid.NewGuid().ToByteArray();
//         var user = new User
//         {
//             Username = email,
//             Password = password,
//             HashKey = key,
//             Role = "Doctor"
//         };
//         _context.Add(user);
//         await _context.SaveChangesAsync();
//         var doctor = new Doctor
//         {
//             Name = "test",
//             YearsOfExperience = 2,
//             Email = email
//         };
//         IRepository<int, Doctor> _doctorRepository = new DoctorRepository(_context);
//         //action
//         await _doctorRepository.Add(doctor);
//         //action


//         //var ex = await Assert.ThrowsAsync<Exception>(() => _doctorRepository.Get(id));

//         //Assert.ThrowsAsync<Exception>(_doctorRepository.Get(id),typeof(System.Exception));

//     }

//     [TearDown]
//     public void TearDown() 
//     {
//         _context.Dispose();
//     }
// }

// ------------------------------------------------------


// using DoctorPatientAppointment.Contexts;
// using DoctorPatientAppointment.Interfaces;
// using DoctorPatientAppointment.Models;
// using DoctorPatientAppointment.Repositories;
// using Microsoft.EntityFrameworkCore;

// public class DoctorRepoTest
// {
//     private ClinicContext _context;

//     [SetUp]
//     public void Setup()
//     {
//         var options = new DbContextOptionsBuilder<ClinicContext>()
//                             .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Isolated DB per test
//                             .Options;
//         _context = new ClinicContext(options);
//     }

//     [Test]
//     public async Task AddDoctorTest()
//     {
//         var doctor = new Doctor
//         {
//             Name = "Dr. Test",
//             YearsOfExperience = 5,
//             Email = "dr.test@example.com"
//         };
//         IRepository<int, Doctor> repo = new DoctorRepository(_context);
//         var result = await repo.Add(doctor);
//         Assert.That(result, Is.Not.Null);
//         Assert.That(result.Id, Is.GreaterThan(0));
//     }

//     [Test]
//     public async Task GetDoctorPassTest()
//     {
//         var doctor = new Doctor
//         {
//             Name = "Dr. Get",
//             YearsOfExperience = 4,
//             Email = "dr.get@example.com"
//         };
//         IRepository<int, Doctor> repo = new DoctorRepository(_context);
//         var added = await repo.Add(doctor);
//         var result = repo.Get(added.Id);
//         Assert.That(result, Is.Not.Null);
//         Assert.That(result.Id, Is.EqualTo(added.Id));
//     }

//     [Test]
//     public void GetDoctorExceptionTest()
//     {
//         IRepository<int, Doctor> repo = new DoctorRepository(_context);
//         Assert.Throws<Exception>(() => repo.Get(999)); // non-existent ID
//     }

//     [Test]
//     public async Task UpdateDoctorTest()
//     {
//         var doctor = new Doctor
//         {
//             Name = "Dr. Update",
//             YearsOfExperience = 3,
//             Email = "dr.update@example.com"
//         };
//         IRepository<int, Doctor> repo = new DoctorRepository(_context);
//         var added = await repo.Add(doctor);
//         added.Name = "Updated Name";

//         var result = await repo.Update(added.Id, added);

//         Assert.That(result.Name, Is.EqualTo("Updated Name"));
//     }

//     [Test]
//     public async Task DeleteDoctorTest()
//     {
//         var doctor = new Doctor
//         {
//             Name = "Dr. Delete",
//             YearsOfExperience = 6,
//             Email = "dr.delete@example.com"
//         };
//         IRepository<int, Doctor> repo = new DoctorRepository(_context);

//         var added = await repo.Add(doctor);
//         var deleted = await repo.Delete(added.Id);

//         Assert.That(deleted, Is.Not.Null);

//         // Assert that exception is thrown when trying to fetch a deleted doctor
//         var ex = Assert.ThrowsAsync<Exception>(async () => await repo.Get(added.Id));
//         Assert.That(ex.Message, Is.EqualTo("Doctor not found")); // Optional: confirm message
//     }

//     [TearDown]
//     public void TearDown()
//     {
//         _context.Dispose();
//     }
// }


