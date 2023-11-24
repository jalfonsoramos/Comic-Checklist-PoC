# ComicChecklist

## A PoC of Minimal Api and .NET MAUI

### Tech Stack

- .NET 7
- C#
- Minimal Api
- .NET MAUI
- Third Party Libraries
  - MediatR

### Scope

1. To develop a minimal api project using clean architecture and cqrs pattern. The functionality scope of the api is for admin and end-user.
2. To develop a ui app using .net maui. The functionality scope of the app is for end-user.

### Use cases

- As an administrator I want to create a checklist with his issues.
- As an administrator I want to view a checklist and his issues.
- As an administrator I want to search checklists by name.
- As an administrator I want to update an existing checklist and change the issues order.

- As an end-user I want to view the available checklists to subscribe
- As an end-user I want to subscribe to an available checklist
- As an end-user I want to view the checklists I'm subscribed and know the progress of the issues.
- As an end-user I want to view a checklist and his issues I'm suscribed.
- As an end-user I want to mark an issue as readed from a checklist I'm subscribed.

### Api Doc

Login Endpoints

| endpoint | verb | description                               |
| -------- | ---- | ----------------------------------------- |
| /token   | POST | Get an access token as Admin or EndUser\* |

Admin Endpoints

| endpoint                   | verb | description                  |
| -------------------------- | ---- | ---------------------------- |
| /admin/checklist           | POST | Create new checklist         |
| /admin/checklist/{id}      | PUT  | Modify an existing checklist |
| /admin/checklist/{id}      | GET  | As get a checklist by id     |
| /admin/checklist?name&page | GET  | As search checklists by name |

End user Endpoints

| endpoint                               | verb | description                                                |
| -------------------------------------- | ---- | ---------------------------------------------------------- |
| /checklist                             | GET  | Get available checklists for subscription                  |
| /checklist/{checklistId}/subscriptions | POST | Subscribe user to checklists                               |
| /checklist/subscriptions               | GET  | Get the checklists the user is subscribed                  |
| /checklist/{checklist}/subscriptions   | GET  | Get the checklist the user is subscribed                   |
| /checklist/{checklist}/subscriptions   | PUT  | Update the issues from a checklists the user is subscribed |

### Notes (READ BEFORE RUN THE APP)

1. The login page in the app simulates the navigation and there is no implementation to enter user name/pwd. For testing purposes an access token is hardcoded.

2. The user credentials verification is also hardcoded in the code for testing purposes. The only valid users to get a token are **Admin/4dm1n** and **User1/us3r**.

3. The api is protected with JWT.

4. Database can be created by default using localdb by executing the command Update-Database in the Package Manager Console. Make sure set the Api as Startup Project and the Data project as Default Project in the Package Manager Console.

5. Look for the sql file seed-data-v1.0.sql in the repository to populate the database with initial and test data.

6. Use dev tunnels to communicate .net maui androd + .net minimal api locally.

Don't forget to set url from dev tunnel in the BaseAddress for the HttpClient in the .net maui.

[Create and host a dev tunnel](https://learn.microsoft.com/en-us/azure/developer/dev-tunnels/get-started?tabs=windows)
