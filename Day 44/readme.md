## Microsoft Azure

## 1. Creation of Virtual Machine in Microsoft Azure

https://portal.azure.com/#home

### Click on Virtual Network

![alt text](image.png)

### Click on Create

![alt text](image-1.png)

![alt text](image-2.png)

Username: rohith
Password: rohith@12345$

![alt text](image-3.png)

### Click on Review + Create

![alt text](image-4.png)

![alt text](image-5.png)

![alt text](image-6.png)

![alt text](image-7.png)
 
### Click on Create

![alt text](image-8.png)

![alt text](image-9.png)

![alt text](image-10.png)

### Go to Home, Virtual Machines

- Click on your VM

![alt text](image-11.png)

- Click on Connect and then Connect

![alt text](image-12.png)

- Download RDP file

![alt text](image-13.png)

![alt text](image-14.png)

- Install Microsoft Remote Desktop
- After installation, click on the rdp file you have installed

![alt text](image-15.png)

- After entering the credentials, your VM will be opened

![alt text](image-16.png)

![alt text](image-17.png)

### Install PostgreSQL on Windows

![alt text](image-18.png)

- Install PostgreSQL version 17.5

![alt text](image-19.png)

- Run the exe file

![alt text](image-20.png)

![alt text](image-21.png)

- Set the password as ROHITH

![alt text](image-22.png)

- Click on Finish, cancel the stack builder

### Create a database in PgAdmin 4

- Open PgAdmin 4

![alt text](image-23.png)

- Create a database named Employees

![alt text](image-24.png)

- Create a table Employee

```
Create TABLE Employee
(
	Employee_Id INT PRIMARY KEY,
	Employee_Name VARCHAR(30)
);
```

![alt text](image-26.png)

```
INSERT INTO Employee 
VALUES(1,'Rohith');
```

![alt text](image-27.png)


```
SELECT *
FROM Employee;
```

![alt text](image-28.png)


## 2. Connect the Backend WebAPI Appplication with the PostgreSQL in VM

### Go to the file location

![alt text](image-30.png)

- Modify the pg_hba.conf in order to allow outside running application to access database present in this VM using the port
- updated the pg_hba.conf file to allow remote access with this rule:

```
host    all             all             0.0.0.0/0               scram-sha-256
```

- Add the above line in the end of pg_hba.conf as I did below and save and close

![alt text](image-31.png)

- Since we are using Azure and VM has NSG (Network Security Group)
- Go to Azure Portal
- Select your VM > Networking
- Under Inbound port rules, add:
```
Source: Any

Source port ranges: *

Destination: Any

Destination port ranges: 5432

Protocol: TCP

Action: Allow

Priority: (e.g., 300)

Name: Allow-Postgres
```

![alt text](image-33.png)

![alt text](image-34.png)

- Click on Create Port Rule

![alt text](image-35.png)

- Select Inbound Port Rule

![alt text](image-36.png)

![alt text](image-37.png)

![alt text](image-38.png)

- Click on Add

![alt text](image-39.png)

- Change priroity to 200

![alt text](image-40.png)

- Click Save

![alt text](image-41.png)

- Now create a separate copy of your backend webapi application and modify the following:
- change your appsettings.json to point to the Azure VM's public IP (74.235.191.75) and correct ports.

### from appsettings.json

```
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "ConnectionStrings": {
        "DefaultConnection": "User ID=postgres;Password=ROHITH;Host=localhost;Port=5432;Database=WareHouseArchiveDb;",
        "WareHouseAuthConnectionString": "User ID=postgres;Password=ROHITH;Host=localhost;Port=5432;Database=WareHouseArchiveAuthDb;"
    },
    "Jwt": {
    "Key": "WareHouseArchiveSuperSecureKey1_2025@1234567890",
    "Issuer": "http://localhost:5239/",
    "Audience": "http://localhost:5239/"
  },
    "AllowedHosts": "*"
}
```

- Copy the public IP address and use it under Host inside appsettings.json

![alt text](image-32.png)

### to appsettings.json

```
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "ConnectionStrings": {
        "DefaultConnection": "User ID=postgres;Password=ROHITH;Host=74.235.191.75;Port=5432;Database=WareHouseArchiveDb;SSL Mode=Prefer;",
        "WareHouseAuthConnectionString": "User ID=postgres;Password=ROHITH;Host=74.235.191.75;Port=5432;Database=WareHouseArchiveAuthDb;"
    },
    "Jwt": {
    "Key": "WareHouseArchiveSuperSecureKey1_2025@1234567890",
    "Issuer": "http://localhost:5239/",
    "Audience": "http://localhost:5239/"
  },
    "AllowedHosts": "*"
}
```

- In windows Vm, open windows Defender Firewall and create a new Rule under Inbound Rules

![alt text](image-42.png)

- CLick on Port

![alt text](image-44.png)

- Protocol : TCP, specific local port: 5432

![alt text](image-45.png)

- Select Allow the connection

![alt text](image-46.png)

- Profile: Check Domain, Private, and Public -> click Next

![alt text](image-47.png)

- Name: PostgreSQL 5432 -> click Finish

![alt text](image-48.png)

- Find the new rule in the list, make sure it's enabled 

![alt text](image-49.png)

### Migrations

- Run the following command

```
dotnet ef migrations add InitialCreate --context WareHouseArchiveAuthDbContext
dotnet ef database update --context WareHouseArchiveAuthDbContext \
  --connection "User ID=postgres;Password=ROHITH;Host=74.235.191.75;Port=5432;Database=WareHouseArchiveAuthDb;SSL Mode=Disable;"
```

![alt text](image-50.png)

![alt text](image-51.png)

![alt text](image-52.png)


```
dotnet ef migrations add InitialCreate --context WareHouseDbContext
dotnet ef database update --context WareHouseDbContext \
--connection "Host=74.235.191.75;Port=5432;Username=postgres;Password=ROHITH;Database=WareHouseArchiveDb;SSL Mode=Prefer;Timeout=15;"
```

![alt text](image-53.png)

![alt text](image-54.png)

![alt text](image-55.png)


### Windows VM PostgreSQL

![alt text](image-56.png)

![alt text](image-57.png)

- Run the backend application from the Mac

![alt text](image-58.png)

![alt text](image-59.png)

![alt text](image-60.png)

- Now check the data of Windows VM PostgreSQL

![alt text](image-61.png)

- The value gets stored in the database

