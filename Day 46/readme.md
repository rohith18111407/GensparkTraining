## Delete previously created Database

- Click Delete

![alt text](image.png)

![alt text](image-1.png)

## Getting started with Azure Boards

https://learn.microsoft.com/en-us/azure/devops/organizations/settings/work/change-process-basic-to-agile?view=azure-devops

- Click on More Services

![alt text](image-2.png)

- Under Devops Select Azure DevOps Organization

![alt text](image-3.png)

![alt text](image-4.png)


https://aex.dev.azure.com/me?mkt=en-GB

![alt text](image-5.png)

- Create new Organization

![alt text](image-6.png)

- Click Continue

![alt text](image-7.png)

```
Organization Name: WareHouseFileArchiver
```

![alt text](image-10.png)

- In advanced Settings, change Work Item Process to Agile and then click Create Project

![alt text](image-11.png)

### Create Epic 1

- Go to Boards > Work Items > New Work Item > Epic and create

![alt text](image-12.png)

- Add Title and Description

```
Epic 1: User Management and Authorization
Description: Implement secure authentication with role-based access and a user management module.
```

![alt text](image-17.png)

- Click Save and Back to Work Items

![alt text](image-18.png)

### Create Feature 1.1

- Go to Backlogs and click on + icon in Epic 1

```
Role-based Authentication
Description: Login system using JWT, refresh tokens, and session storage for role-based redirection.
```

![alt text](image-19.png)

- Click Save and close

![alt text](image-20.png)

### Create Feature 1.2

- Go to Backlogs and click on + icon in Epic 1

```
Role-based Authentication
Description: Login system using JWT, refresh tokens, and session storage for role-based redirection.
```

![alt text](image-21.png)

- Click Save and close

![alt text](image-22.png)


### Create User Story 1.1.1

- Go to Backlogs and click on + icon in Feature 1.1

```
User Story 1.1.1: Login with JWT and sessionStorage
As a Staff/Admin, I want to log in and store my JWT so I can access protected routes.
```

![alt text](image-23.png)

- Click Save and Close

![alt text](image-24.png)


### Create User Story 1.1.2

- Go to Backlogs and click on + icon in Feature 1.1

```
User Story 1.1.2: Auto redirect based on roles
As a user, I want to be redirected based on role so that I only see allowed content.
```

![alt text](image-25.png)

- Click Save and Close

![alt text](image-26.png)


### Create User Story 1.2.1

- Go to Backlogs and click on + icon in Feature 1.1

```
User Story 1.2.1: Add, update, and delete users
As an Admin, I want to manage user accounts to control access.
```

![alt text](image-27.png)

- Click Save and Close

![alt text](image-28.png)

### Create User Story 1.2.2

- Go to Backlogs and click on + icon in Feature 1.1

```
User Story 1.2.2: Prevent Admin from deleting self
As an Admin, I want safeguards to avoid deleting my own account.
```

![alt text](image-29.png)

- Click Save and Close

![alt text](image-30.png)



### Create Epic 2

- Go to Boards > Work Items > New Work Item > Epic and create

![alt text](image-31.png)

- Add Title and Description

```
EPIC 2: File Archival and Version Control
Description: Enable upload, categorization, and version control of files linked to items.
```

![alt text](image-32.png)

- Click Save and Back to Work Items

![alt text](image-33.png)


### Create Feature 2.1

- Go to Backlogs and click on + icon in Epic 2

```
File Upload & Categorization
Description: Support file upload with categories and versioning, linked to items.
```

![alt text](image-34.png)

- Click Save and close

![alt text](image-35.png)

### Create Feature 2.2

- Go to Backlogs and click on + icon in Epic 2

```
Archive File Listing & Grouping
Description: Ability to list, group, and search files by item and category.
```

![alt text](image-36.png)

- Click Save and close

![alt text](image-37.png)

### Create User Story 2.1.1

- Go to Backlogs and click on + icon in Feature 2.1

```
User Story 2.1.1: Upload file with versioning
As an Admin, I want to upload files and manage versions under items.
```

![alt text](image-38.png)

- Click Save and Close

![alt text](image-39.png)


### Create User Story 2.1.2

- Go to Backlogs and click on + icon in Feature 2.1

```
User Story 2.1.2: Validate file type and size
As an Admin, I want to allow only specific file types to ensure security.
```
![alt text](image-40.png)

- Click Save and Close

![alt text](image-41.png)


### Create User Story 2.2.1

- Go to Backlogs and click on + icon in Feature 2.2

```
User Story 2.2.1: Search files by name or category
As a user, I want to search archived files to find relevant versions.
```

- Click Save and Close

![alt text](image-42.png)

### Create User Story 2.2.2

- Go to Backlogs and click on + icon in Feature 2.2

```
User Story 2.2.2: Group files by category
As a user, I want files grouped by category for better browsing.
```

![alt text](image-43.png)

- Click Save and Close

![alt text](image-44.png)


### Create Tasks for User Story 1.1.1 : Login with JWT and sessionStorage

- Go to Backlogs and click on + icon in User Story 1.1.1

![alt text](image-45.png)

```
T1.1.1	Design login form with reactive validation in Angular
```

![alt text](image-46.png)

- Click Save and Close

![alt text](image-47.png)

```
T1.1.2	Implement JWT authentication endpoint in ASP.NET Core
```

![alt text](image-48.png)

- Click Save and Close

![alt text](image-49.png)

```
T1.1.3	Store JWT and refresh token in sessionStorage after login
```

![alt text](image-50.png)

- Click Save and Close

![alt text](image-51.png)


### Create Tasks for User Story 1.1.2 : Auto redirect based on roles

- Go to Backlogs and click on + icon in User Story 1.1.2

![alt text](image-52.png)

```
T1.1.4	Store user role in sessionStorage after login
```

![alt text](image-53.png)

- Click Save and Close

![alt text](image-54.png)

```
T1.1.5	Implement Angular route guards for Admin and Staff
```

![alt text](image-55.png)

- Click Save and Close

![alt text](image-56.png)

```
T1.1.6	Redirect users to appropriate dashboard based on role
```

![alt text](image-57.png)

- Click Save and Close

![alt text](image-58.png)


### Create Tasks for User Story 1.2.1 : Add, update, and delete users

- Go to Backlogs and click on + icon in User Story 1.2.1

![alt text](image-80.png)

```
T1.2.1	Create backend API endpoints for CRUD operations
```

![alt text](image-81.png)

- Click Save and Close

![alt text](image-82.png)

```
T1.2.2	Build Angular UI for managing users with modal forms
```

![alt text](image-83.png)

- Click Save and Close

![alt text](image-84.png)

```
T1.2.3	Validate duplicate usernames and show error feedback
```

![alt text](image-85.png)

- Click Save and Close

![alt text](image-86.png)


### Create Tasks for User Story 1.2.2 : Prevent Admin from deleting self

- Go to Backlogs and click on + icon in User Story 1.2.2

![alt text](image-87.png)

```
T1.2.4	Add check in delete endpoint to block self-deletion
```

![alt text](image-88.png)

- Click Save and Close

![alt text](image-89.png)

```
T1.2.5	Disable delete button for currently logged-in Admin
```

![alt text](image-90.png)

- Click Save and Close

![alt text](image-91.png)

```
T1.2.6	Write unit tests for self-delete prevention logic
```

![alt text](image-92.png)

- Click Save and Close

![alt text](image-93.png)


### Create Tasks for User Story 2.1.1: Upload file with versioning

- Go to Backlogs and click on + icon in User Story 2.1.1


```
T2.1.1	Create API to upload file with version tracking
T2.1.2	Add Angular file upload UI with item/category selection
T2.1.3	Save metadata: ItemId, Category, VersionNumber in DB
```


### Create Tasks for User Story 2.1.2: Validate file type and size

- Go to Backlogs and click on + icon in User Story 2.1.2

![alt text](image-73.png)

```
T2.1.4	Validate file extension on frontend before upload
T2.1.5	Enforce max file size on backend (e.g., 10MB)
T2.1.6	Show error alert if validation fails before/after upload
```


### Adding Bugs

![alt text](image-119.png)

![alt text](image-120.png)

![alt text](image-121.png)

![alt text](image-122.png)

![alt text](image-123.png)

### Create Sprints

![alt text](image-97.png)

- Click on Create

![alt text](image-98.png)

- From Backlog, drag each of the task into the sprints present in the right side

![alt text](image-99.png)

- Sprint 1

![alt text](image-124.png)

![alt text](image-125.png)

![alt text](image-101.png)

![alt text](image-102.png)

![alt text](image-103.png)

![alt text](image-104.png)

![alt text](image-105.png)

- Sprint 2

![alt text](image-106.png)

![alt text](image-107.png)

![alt text](image-108.png)

![alt text](image-109.png)

- Drag few sprints to Closed and Active based on your needs

- Sprint - 1 

![alt text](image-114.png)

![alt text](image-115.png)

- Sprint - 2

![alt text](image-116.png)

![alt text](image-117.png)

![alt text](image-118.png)




### Kanban Status

- In your Kanban board (Boards > Boards), move the following 3 tasks to In Progress:

```
Task 1: Create backend API for file upload with version control
Task 2: Create Angular UI with progress bar
Task 3: Store file details with metadata
```
- Stories

![alt text](image-126.png)

![alt text](image-127.png)

- Drag the stories based on your progress

![alt text](image-128.png)

- Features

![alt text](image-112.png)

- Epics

![alt text](image-113.png)


## Class diagram

![alt text](image-129.png)


## Sequence Diagram

![alt text](image-130.png)