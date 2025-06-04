#  Doctor-Patient Appointment Management System

A RESTful web API built using ASP.NET Core and Entity Framework Core for managing doctors, patients, appointments functionalities in a clinic or hospital environment.

---

##  Features

-  **Doctor Management**
  - Add, update, delete doctors
  - Filter doctors by speciality
  - Doctors can view the following post authentication:
   1) GET: /api/Doctor
   2) GET: /api/Patient
   3) POST: /api/Authentication
   4) POST: /api/Doctor
   5) POST: /api/Patient

- 👩‍⚕️ **Patient Management**
  - Register and manage patient profiles
  - Secure authentication with JWT
  - Patientss can view the following post authentication:
   1) GET: /api/Doctor
   2) GET: /api/Patient
   3) GET: /api/Patient/{id}
   4) POST: /api/Authentication
   5) POST: /api/Doctor
   6) POST: /api/Patient

- **Appointments**
  - Book appointments between patients and doctors
  - View appointment history

- **Authentication & Authorization**
  - JWT-based secure login for doctors, patients, and admin
  - Role-based access control

- **Database**
  - PostgreSQL database with Entity Framework Core
  - Stored procedure support (e.g., filter doctors by speciality)

---

## Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- JWT (JSON Web Tokens)
- Swagger UI
- LINQ, Asynchronous Programming

---

## Output

### Post Doctor

![alt text](image.png)

![alt text](image-1.png)

### Authenticate Doctor

![alt text](image-2.png)

![alt text](image-3.png)

- Copy the token and paste it in the lock icon

![alt text](image-4.png)

- Paste the token and authorize

![alt text](image-5.png)

![alt text](image-6.png)

- Paste the token and authorize GET: /api/Patient

![alt text](image-7.png)

![alt text](image-8.png)


- Paste the token and authorize GET: /api/Patient/{id}

![alt text](image-9.png)

![alt text](image-10.png)


### Post Patient

![alt text](image-11.png)

![alt text](image-12.png)

### Authorize Patient

![alt text](image-13.png)

![alt text](image-14.png)

- Paste the token and authorize GET: /api/Doctor

![alt text](image-15.png)

![alt text](image-16.png)

![alt text](image-17.png)

- Paste the token and authorize GET: /api/Patient

![alt text](image-18.png)

![alt text](image-19.png)

- Paste the token and authorize GET: /api/Patient/{id}

![alt text](image-20.png)

![alt text](image-21.png)


### OAuth Implementation

- Before Login, User table

![alt text](image-27.png)

- Then perform the following

![alt text](image-22.png)

- go to https://localhost:7234/auth/GoogleResponse

![alt text](image-23.png)

- you will get the token and use it at the APIs that are enclosed with [Authorize]

![alt text](image-24.png)

- Execute it

![alt text](image-26.png)

![alt text](image-25.png)

- You will be seeing the token

- Now the User table looks like the following

![alt text](image-28.png)

### Unit Testing

- Run 1 test at a time by executing the following command in terminal

```
dotnet test
```

### Commands

```
dotnet add package NUnit
dotnet add package NUnit3TestAdapter
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package Microsoft.EntityFrameworkCore.InMemory

dotnet add package Moq 

dotnet add package Microsoft.Extensions.Logging.Log4Net.AspNetCore

dotnet add package Microsoft.AspNetCore.Authentication.Google


dotnet restore

dotnet clean
dotnet restore
dotnet build
dotnet test
```


