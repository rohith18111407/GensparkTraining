using AutoMapper;
using DoctorPatientAppointment.Contexts;
using DoctorPatientAppointment.Interfaces;
using DoctorPatientAppointment.Models;
using DoctorPatientAppointment.Models.DTOs;
using DoctorPatientAppointment.Repositories;
using DoctorPatientAppointment.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

[TestFixture]
public class PatientServiceTest
{
    private ClinicContext _context;
    private Mock<IRepository<int, Patient>> _patientRepoMock;
    private Mock<IRepository<string, User>> _userRepoMock;
    private Mock<IEncryptionService> _encryptionServiceMock;
    private Mock<IMapper> _mapperMock;
    private PatientService _patientService;

    [SetUp]
    public void Setup()
    {
        _patientRepoMock = new Mock<IRepository<int, Patient>>();
        _userRepoMock = new Mock<IRepository<string, User>>();
        _encryptionServiceMock = new Mock<IEncryptionService>();
        _mapperMock = new Mock<IMapper>();

        _patientService = new PatientService(
            _patientRepoMock.Object,
            _userRepoMock.Object,
            _encryptionServiceMock.Object,
            _mapperMock.Object
        );
    }

    // [Test]
    // public async Task AddPatientTest()
    // {
    //     // Arrange
    //     var dto = new PatientAddRequestDto
    //     {
    //         Name = "Patient A",
    //         Age = 25,
    //         Phone = "1111111111",
    //         Email = "patientA@example.com",
    //         Password = "password123"
    //     };

    //     var encryptedModel = new EncryptModel
    //     {
    //         EncryptedData = new byte[] { 1, 2, 3 },
    //         HashKey = new byte[] { 4, 5, 6 }
    //     };

    //     var mappedUser = new User
    //     {
    //         Username = dto.Email,
    //         Role = "Patient",
    //         Password = encryptedModel.EncryptedData,
    //         HashKey = encryptedModel.HashKey
    //     };

    //     var patientToReturn = new Patient
    //     {
    //         Name = dto.Name,
    //         Age = dto.Age,
    //         Email = dto.Email,
    //         Phone = dto.Phone,
    //         User = mappedUser
    //     };

    //     _mapperMock.Setup(m => m.Map<PatientAddRequestDto, User>(dto)).Returns(mappedUser);
    //     _encryptionServiceMock
    //         .Setup(e => e.EncryptData(It.IsAny<EncryptModel>()))
    //         .ReturnsAsync(encryptedModel);
    //     _userRepoMock.Setup(r => r.Add(It.IsAny<User>())).ReturnsAsync(mappedUser);
    //     _patientRepoMock.Setup(r => r.Add(It.IsAny<Patient>())).ReturnsAsync(patientToReturn);

    //     // Act
    //     var result = await _patientService.AddPatient(dto);

    //     // Assert
    //     Assert.That(result, Is.Not.Null);
    //     Assert.That(result.Email, Is.EqualTo(dto.Email));
    //     Assert.That(result.Name, Is.EqualTo(dto.Name));

    //     _mapperMock.Verify(m => m.Map<PatientAddRequestDto, User>(dto), Times.Once);
    //     _encryptionServiceMock.Verify(e => e.EncryptData(It.IsAny<EncryptModel>()), Times.Once);
    //     _userRepoMock.Verify(r => r.Add(It.Is<User>(u => u.Username == dto.Email)), Times.Once);
    //     _patientRepoMock.Verify(r => r.Add(It.Is<Patient>(p => p.Email == dto.Email)), Times.Once);
    // }


    // [Test]
    // public async Task GetPatientByIdTest()
    // {
    //     // Arrange
    //     var patient = new Patient
    //     {
    //         Id = 1,
    //         Name = "Existing Patient",
    //         Age = 40,
    //         Phone = "2222222222",
    //         Email = "existing@patient.com"
    //     };

    //     _patientRepoMock.Setup(repo => repo.Get(patient.Id)).ReturnsAsync(patient);

    //     // Act
    //     var result = await _patientService.GetPatientById(patient.Id);

    //     // Assert
    //     Assert.That(result, Is.Not.Null);
    //     Assert.That(result.Email, Is.EqualTo("existing@patient.com"));
    //     Assert.That(result.Name, Is.EqualTo("Existing Patient"));

    //     _patientRepoMock.Verify(repo => repo.Get(patient.Id), Times.Once);
    // }


    [Test]
    public void GetPatientByInvalidId_ThrowsException()
    {
        Assert.ThrowsAsync<Exception>(async () => await _patientService.GetPatientById(999));
    }

    [TearDown]
    public void TearDown()
    {
        _context?.Dispose();
    }
}
