using WareHouseFileArchiver.Models.Domains;

namespace WareHouseFileArchiver.Interfaces
{
    public interface IArchiveFileRepository
    {
        Task<ArchiveFile> UploadAsync(ArchiveFile archiveFile);
        Task<IEnumerable<ArchiveFile>> GetFilesByItemIdAsync(Guid itemId, bool includeArchived = false);
        Task<bool> ItemExistsAsync(Guid itemId);
        Task<ArchiveFile?> GetLatestVersionAsync(string fileName, Guid itemId, bool includeArchived = false);
        Task UpdateAsync(ArchiveFile file);
        Task<ArchiveFile?> GetFileByNameAndVersionAsync(string filename, int version);
        Task<Item?> GetItemByIdAsync(Guid itemId);
        Task<IEnumerable<ArchiveFile>> GetAllFilesAsync(bool includeArchived);
        Task<ArchiveFile?> GetFileByIdAsync(Guid id);
        Task DeleteAsync(ArchiveFile file);
        Task LogDownloadAsync(FileDownloadLog log);


        // Scheduled File upload methods
        Task<IEnumerable<ArchiveFile>> GetScheduledFilesAsync();
        Task<IEnumerable<ArchiveFile>> GetPendingScheduledFilesAsync();
        Task<ArchiveFile?> GetScheduledFileByIdAsync(Guid id);
        Task UpdateScheduledFileAsync(ArchiveFile archiveFile);
        Task DeleteScheduledFileAsync(Guid id);

        // Trash/Soft Delete methods
        Task<IEnumerable<ArchiveFile>> GetTrashedFilesAsync();
        Task MoveToTrashAsync(ArchiveFile file, string deletedBy, string trashFilePath);
        Task RestoreFromTrashAsync(Guid fileId, string newFilePath);
        Task PermanentlyDeleteAsync(Guid fileId);
        Task<IEnumerable<ArchiveFile>> GetExpiredTrashedFilesAsync(int daysOld = 7);

        //new methods for archival 
        Task<IEnumerable<ArchiveFile>> GetArchivedFilesAsync();
        Task<IEnumerable<ArchiveFile>> GetArchivedFilesByAdminAsync(string adminName);
        Task<bool> UnarchiveFileAsync(Guid fileId, string unarchiveBy);
        Task<bool> ArchiveFileManuallyAsync(Guid fileId, string archiveBy, string reason);
        Task<Dictionary<string, int>> GetArchivalStatsByAdminAsync();
        Task<int> ArchiveInactiveUsersFilesAsync(int inactiveDaysThreshold);

         Task<int> ArchiveInactiveAdminFilesAsync(List<string> inactiveAdminUsernames, string archiveReason = null);
        Task<IEnumerable<ArchiveFile>> GetFilesForInactiveAdminsAsync(List<string> inactiveAdminUsernames);
    }
}