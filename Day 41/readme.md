#  WareHouse File Archiver

The **WareHouse File Archiver** is a web-based file management and archiving system built with **.NET Web API**, **PostgreSQL**, and **Angular**. It is designed to provide secure file storage with version control, scheduled uploads, trash management, archiving, and real-time notifications. The application implements **role-based access control** for Admins and Staff and integrates **SignalR** for live notifications along with email alerts for file-related activities.

---

## Tech Stack

### Backend
- **.NET 9 Web API** – RESTful API development.
- **Entity Framework Core** – Database ORM.
- **PostgreSQL** – Relational database.
- **JWT Authentication** – Secure authentication using JSON Web Tokens.
- **Identity** – Role-based authorization (Admin & Staff).
- **SignalR** – Real-time notifications.
- **Email Service** – Email notifications for file activities.

### Frontend
- **Angular 20+** – Single-page application framework.
- **Bootstrap** – Modern UI styling and responsive design.
- **SignalR Client** – Real-time notification handling in the UI.

### Other Tools
- **NUnit** – Unit testing framework for .NET.
- **Swagger (OpenAPI)** – Interactive API documentation and testing.
- **Git** – Version control.

---

## Features

### Authentication & Authorization
- Secure **JWT-based login**.
- **Role-based access control**:
  - **Staff** can self-register via the signup feature.
  - **Admins** can create both Admin and Staff users.
- **Refresh tokens** for maintaining authenticated sessions.

---

### File Management
- **Upload Files** under specific items with file categories.
- **Version Control** – Automatically increments file version when re-uploaded.
- **Scheduled Uploads** – Admin can schedule a file upload for a specific date and time.
- **Bulk Downloads** – Download multiple files as a ZIP.
- **Download Tracking** – Notifications for individual and bulk downloads.

---

### Archiving & Trash
- **Manual Archiving** – Admin can archive a file by specifying a reason.
- **Automatic Archiving** – Files of owners inactive for 30 days are automatically archived.
- **Unarchive Files** – Admin can restore archived files when required.
- **Trash Management** – Deleted files:
  - Are moved to Trash for **7 days**.
  - Can be **restored** within 7 days.
  - Are **permanently deleted** after 7 days if not restored.

---

### Item & Category Management (Admin Only)
- Create, edit, and delete **items**.
- Manage **item categories** for organized file storage.

---

### User Management
- **Admin**:
  - Create, edit, and delete users.
  - Assign roles (Admin/Staff).
- **Staff**:
  - Can only register as **Staff** using the signup feature.

---

### Notifications
- **Email Notifications** sent to all users for:
  - File uploads.
  - Scheduled uploads.
  - File version updates.
- **Real-time Notifications (SignalR)**:
  - File uploaded.
  - File downloaded.
  - Bulk files downloaded.

---

### Staff Features
- View all items and their associated files.
- Download individual files.
- Bulk download files as ZIP.
- View files archived within the last 2 days.

---

## Admin vs. Staff Features Comparison

| Feature                       | Admin | Staff |
|-------------------------------|:-----:|:-----:|
| User Management               | ✅    | ❌    |
| Item & Category Management    | ✅    | ❌    |
| File Upload                   | ✅    | ❌    |
| Scheduled Uploads             | ✅    | ❌    |
| File Versioning               | ✅    | ❌    |
| File Download                 | ✅    | ✅    |
| Bulk File Download            | ✅    | ✅    |
| Archive (Manual/Auto)         | ✅    | View Last 2 Days |
| Unarchive Files               | ✅    | ❌    |
| Trash Management              | ✅    | ❌    |
| Notifications (SignalR)       | ✅    | ✅    |
| Email Notifications           | ✅    | ✅    |

---

## File Lifecycle

1. **Upload** → File is uploaded under a specific item with a category.
2. **Versioning** → If the file exists, a new version is created.
3. **Archiving** → File is archived manually by admin or automatically after 30 days of inactivity.
4. **Trash** → If deleted, file moves to trash for 7 days.
5. **Permanent Delete** → After 7 days in trash, file is permanently removed.

---

## Real-Time Notifications

- **Admin uploads/schedules a file or updates a file version** → All users receive an **email notification**.
- **SignalR notifications**:
  - File uploaded.
  - File downloaded.
  - Bulk downloads.

This ensures real-time updates without refreshing the application.

---

## Testing

- **Unit Tests with NUnit**:
  - Repository layer tests using in-memory databases.
  - User, file, and item management test coverage.
  - Archiving and trash management validations.

---

## API Documentation

- Fully documented APIs using **Swagger**.
- Interactive UI for testing endpoints.

---

## Security

- JWT authentication for all API endpoints.
- Role-based authorization for Admin and Staff.
- Audit columns to track file activities: `CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy`.

---

## Notifications Workflow

1. **File Upload/Schedule/Update** → Triggers backend event.
2. **Email Service** → Sends email to all users.
3. **SignalR** → Pushes live notifications to all connected clients.

---

## Roles Summary

- **Admin**: Full control over users, items, files, archiving, trash, notifications.
- **Staff**: Limited access for viewing and downloading files and viewing recent archives.

---

## Output screenshots:

- Login Page

![alt text](image.png)

- Sign Up page

![alt text](image-1.png)

- Admin Dashboard

![alt text](image-2.png)

![alt text](image-3.png)

- Admin Files Section

![alt text](image-4.png)

![alt text](image-14.png)

- Admin Archived Files Section

![alt text](image-5.png)

- Admin Upload File Section

![alt text](image-6.png)

- Admin Schedule Upload Section

![alt text](image-7.png)

![alt text](image-8.png)

- Admin Items Section

![alt text](image-9.png)

![alt text](image-13.png)

- Admin Add Item Section

![alt text](image-10.png)

- Admin Users Section

![alt text](image-11.png)

![alt text](image-12.png)

- Admin Trash Section

![alt text](image-15.png)

![alt text](image-16.png)

- Admin Statistics Section

![alt text](image-17.png)

![alt text](image-18.png)

![alt text](image-19.png)

- Admin Bulk Download

![alt text](image-20.png)

- Staff Dashboard

![alt text](image-21.png)

![alt text](image-22.png)

- Staff files section

![alt text](image-23.png)

![alt text](image-24.png)

- Staff Archived Files Section

![alt text](image-25.png)

![alt text](image-26.png)

- Staff Bulk Download

![alt text](image-27.png)

![alt text](image-28.png)

- Staff Items Section

![alt text](image-29.png)

![alt text](image-30.png)

- Staff Statistics Section

![alt text](image-31.png)

![alt text](image-32.png)

![alt text](image-33.png)

- Email Notification

![alt text](image-34.png)

![alt text](image-35.png)

![alt text](image-36.png)

## Conclusion

The **WareHouse File Archiver** simplifies file management by combining:
- Version control,
- Archiving automation,
- Trash recovery,
- User management,
- Real-time and email notifications.

This ensures secure and organized file handling while providing a clear separation of roles between Admins and Staff.

---

