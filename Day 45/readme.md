## Deletion of previously created VM

![alt text](image.png)

- Click on your VM and press delete

![alt text](image-1.png)


- Click Delete

![alt text](image-2.png)

![alt text](image-3.png)

- Deleted successfully

![alt text](image-4.png)


## Creation of Linux VM on Azure

![alt text](image-5.png)

- Click Create and VIrtual Machine

![alt text](image-9.png)

![alt text](image-6.png)

![alt text](image-7.png)

```
username: rohith
Key pair name: rohith
```

![alt text](image-8.png)

- Click on Review and Create

![alt text](image-10.png)

- Click Create

![alt text](image-11.png)

- Click Download Private Key and create resource

![alt text](image-12.png)

![alt text](image-13.png)

![alt text](image-14.png)

- You could see the VM is successfully created

![alt text](image-15.png)

- Click on your VM and Click Connect

![alt text](image-16.png)

![alt text](image-17.png)

- Select Native SSH

![alt text](image-18.png)


- Under Copy and execute SSH command, switch to local machine OS as macOS 

![alt text](image-21.png)

- Already downloaded file is rohith.pem

![alt text](image-20.png)

- Open terminal in macOS from the location where rohith.pem is present

```
chmod 400 rohith.pem 
sudo ssh -i rohith.pem rohith@40.90.249.74 
```

![alt text](image-22.png)
![alt text](image-23.png)

- Now your ubuntu VM terminal will be opened, run the following commands

## Installation of Docker in Ubuntu VM

```
sudo apt-get update
sudo apt-get install -y docker.io
```

![alt text](image-24.png)

![alt text](image-25.png)

- Start Docker and enable it on boot:

```
sudo systemctl start docker
sudo systemctl enable docker
```

![alt text](image-26.png)

- Verify Docker Installation

```
docker --version
```

![alt text](image-27.png)


### Now in your Local Machine(Mac), create a new >Net Web API Project

```
dotnet new webapi -o MyApiApp
cd MyApiApp
dotnet add package Swashbuckle.AspNetCore
```
![alt text](image-28.png)

![alt text](image-33.png)

- Program.cs

```
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Enable minimal API Swagger support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyApiApp v1");
        c.RoutePrefix = string.Empty; // this makes Swagger UI available at '/'
    });
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// Optional: Root route message (can be removed since Swagger is now root)
app.MapGet("/", () => "Welcome to MyApiApp!");

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        )).ToArray();

    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi(); // enables Swagger doc generation for this endpoint

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

```

- Test locally with:

```
dotnet run
```

![alt text](image-30.png)

Visit:

http://localhost:5106/index.html

![alt text](image-34.png)

![alt text](image-35.png)

![alt text](image-36.png)

### Dockerfile

```
# ---- Stage 1: Build ----
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy project files and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# ---- Stage 2: Runtime ----
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy the build output from the previous stage
COPY --from=build /app/out ./

# Expose ports (match your launchSettings.json)
EXPOSE 5106
EXPOSE 7049

# Run the app
ENTRYPOINT ["dotnet", "MyApiApp.dll"]
```

### terminal

- Build the image

```
docker build -t myapiapp .
```

![alt text](image-37.png)

- Login to docker Hub

```
docker login
```
![alt text](image-38.png)

- Tag the image for Docker Hub

```
docker tag myapiapp rohith18111407/myapiapp:latest
```

![alt text](image-39.png)

- Push to Docker Hub

```
docker push rohith18111407/myapiapp:latest
```
![alt text](image-40.png)

### Set the inbound rule for the Azure VM

![alt text](image-47.png)

- Click on Create Inbound Port Rule

![alt text](image-48.png)

- Click on Add

![alt text](image-49.png)

![alt text](image-50.png)

![alt text](image-51.png)

### Pull and Run the API on Azure VM

```
docker login
```

![alt text](image-41.png)

- Let me use the already existing public image 

- Visit:

https://hub.docker.com/repositories/rohith18111407

![alt text](image-42.png)

![alt text](image-43.png)

- Run the following commmand:

```
sudo docker pull rohith18111407/react-docker:latest
```

![alt text](image-44.png)


```
sudo docker run -d -p 5173:5173 rohith18111407/react-docker:latest
```

![alt text](image-45.png)

- Use the public ip address:

![alt text](image-46.png)

- Visit:

http://40.90.249.74:5173/

![alt text](image-52.png)

- Now the react app is running using the http://40.90.249.74:5173/

![alt text](image-53.png)

- Verify the container is running

```
sudo docker images
sudo docker ps
sudo docker ps -a
```

![alt text](image-54.png)


### react-docker application present in docker-hub

### React Application

```
npm create vite@latest react-docker
```

Select framework : react
Select variant : Typescript

### Dockerfile

```
# set the base image to create the image for react app
FROM node:20-alpine

# create a user with permissions to run the app
# -S -> create a system user
# -G -> add the user to a group
# This is done to avoid running the app as root
# If the app is run as root, any vulnerability in the app can be exploited to gain access to the host system
# It's a good practice to run the app as a non-root user
RUN addgroup app && adduser -S -G app app

# set the user to run the app
USER app

# set the working directory to /app
WORKDIR /app

# copy package.json and package-lock.json to the working directory
# This is done before copying the rest of the files to take advantage of Docker’s cache
# If the package.json and package-lock.json files haven’t changed, Docker will use the cached dependencies
COPY package*.json ./

# sometimes the ownership of the files in the working directory is changed to root
# and thus the app can't access the files and throws an error -> EACCES: permission denied
# to avoid this, change the ownership of the files to the root user
USER root

# change the ownership of the /app directory to the app user
# chown -R <user>:<group> <directory>
# chown command changes the user and/or group ownership of for given file.
RUN chown -R app:app .

# change the user back to the app user
USER app

# install dependencies
RUN npm install

# copy the rest of the files to the working directory
COPY . .

# expose port 5173 to tell Docker that the container listens on the specified network ports at runtime
EXPOSE 5173

# command to run the app
CMD npm run dev
```

### .dockerignore

```
node_modules/
```

### Push the react-docker image in Docker Hub

```
docker tag react-docker rohith18111407/react-docker
docker push rohith18111407/react-docker 
```

Visit:

docker.hub.com

- Now the image is available in Docker Hub and other people can run this image and containerize the application



## Creating Azure SQL Database

```
brew update && brew install azure-cli
 
az login

az account show
```

- Click on SQL Databases

![alt text](image-55.png)

- Click Create

![alt text](image-56.png)

![alt text](image-57.png)

- Server: Create New

![alt text](image-58.png)

![alt text](image-59.png)

- Set Microsoft Entra admin : Click Set Admin

- Click on M, Rohith

![alt text](image-60.png)

- Click Select

![alt text](image-61.png)

- Click Ok

![alt text](image-62.png)

![alt text](image-63.png)

![alt text](image-64.png)

- Click on Compute + Storage, Configure Database

![alt text](image-65.png)

- Click on Serverless under Compute Tier

![alt text](image-66.png)

- Click On Apply

![alt text](image-67.png)

- Click Next: Networking

- Change Connectivity Method to Public Endpoint

![alt text](image-70.png)

- Click Next: Security

- Click: Review+Create

![alt text](image-71.png)

![alt text](image-72.png)

- Click Download a template for automation

![alt text](image-73.png)

![alt text](image-74.png)

- Click Download

![alt text](image-75.png)

![alt text](image-76.png)

- Close the template 
- Click Create

![alt text](image-78.png)

![alt text](image-79.png)

![alt text](image-80.png)

![alt text](image-81.png)

- The template gets downloaded

![alt text](image-77.png)

- Open terminal at the location where parameters.json and template.json is present

```
az deployment group create --resource-group Rohith_GenSparkTraining --template-file template.json --parameters @parameters.json
```

![alt text](image-82.png)

![alt text](image-83.png)

![alt text](image-84.png)

![alt text](image-85.png)

![alt text](image-86.png)

![alt text](image-87.png)

![alt text](image-88.png)

- Database gets created

![alt text](image-89.png)

![alt text](image-90.png)

- Click Set Server Firewall

![alt text](image-91.png)

- Click on Add your current IPV4 Address

![alt text](image-92.png)

- Tick the checkbox and click on Save

![alt text](image-93.png)

![alt text](image-94.png)

- Under Query Editor, Click on Login as rohithm@presidio.com

![alt text](image-95.png)

![alt text](image-96.png)

- Click on Getting Started or refer 

https://learn.microsoft.com/en-us/azure/azure-sql/database/connect-query-portal?view=azuresql#troubleshooting-and-considerations

```
SELECT SYSDATETIMEOFFSET(), DB_NAME(), ORIGINAL_LOGIN();
```

![alt text](image-97.png)



## Deleting existing Ubuntu VM

- Go to Virtual Machines, select your VM

![alt text](image-98.png)

- Click on Delete

![alt text](image-99.png)

![alt text](image-100.png)
