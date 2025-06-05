# NotifyAPI - Real-Time Document Notification System

NotifyAPI is an ASP.NET Core Web API application that allows HR users to upload documents and instantly notifies all connected staff users in real-time using SignalR.

---

## Features

- JWT-based authentication & authorization
- Secure document upload for HR role
- View document list for Staff role
- Real-time notifications to connected users via SignalR
- CORS configured for external frontend access
- PostgreSQL for persistent data storage
- Swagger UI for API testing

---

## Technologies Used

- ASP.NET Core Web API
- Entity Framework Core (PostgreSQL)
- SignalR
- JWT Authentication
- Identity & Roles
- Swagger
- HTML + JavaScript (for front-end demo)

---

## Output

- Register using POST: /api/Auth/Register

![alt text](image.png)

![alt text](image-1.png)

- Now login POST: /api/Auth

![alt text](image-2.png)

- Authorize

![alt text](image-3.png)

- POST /api/Documents/Upload as a HR

![alt text](image-4.png)

![alt text](image-5.png)

- Open Test.html and activate live server

![alt text](image-6.png)

- HR cannot use the GET /api/Documents

![alt text](image-8.png)

- Login as staff

![alt text](image-7.png)

![alt text](image-10.png)

![alt text](image-11.png)

![alt text](image-12.png)

- Copy The token and authorize as Staff

![alt text](image-13.png)

![alt text](image-14.png)

![alt text](image-15.png)

- Staff dont have permission to upload documents

![alt text](image-16.png)

![alt text](image-17.png)
