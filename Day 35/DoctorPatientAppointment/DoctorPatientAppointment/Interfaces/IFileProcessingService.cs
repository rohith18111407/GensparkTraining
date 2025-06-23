using DoctorPatientAppointment.Models.DTOs;

namespace DoctorPatientAppointment.Interfaces
{
    public interface IFileProcessingService
    {
        public Task<FileUploadReturnDTO> ProcessData(CsvUploadDto csvUploadDto);
    }
}