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
- As an administrator I want to view a checklist and  his issues.
- As an administrator I want to search checklists by name.
- As an administrator I want to update an existing checklist and change the issues order.

- As an end-user I want to view the available checklists to subscribe
- As an end-user I want to subscribe to an available checklist
- As an end-user I want to view the checklists I'm subscribed and know the progress of the issues.
- As an end-user I want to view a checklist and his issues I'm suscribed.
- As an end-user I want to mark an issue as readed from a checklist I'm subscribed.

### Api Doc

Login Endpoints

| endpoint | verb | description                              |
| -------- | ---- | ---------------------------------------- |
| /token   | POST | Get an access token as Admin or EndUser* |

Admin Endpoints

| endpoint                   | verb | description                  |
| -------------------------- | ---- | ---------------------------- |
| /admin/checklist           | POST | Create new checklist         |
| /admin/checklist/{id}      | PUT  | Modify an existing checklist |
| /admin/checklist/{id}      | GET  | As get a checklist by id     |
| /admin/checklist?name&page | GET  | As search checklists by name |

End user Endpoints

| endpoint                               | verb | description                               |
| -------------------------------------- | ---- | ----------------------------------------- |
| /checklist                             | GET  | Get available checklists for subscription |
| /checklist/{checklistId}/subscriptions | POST | Subscribe user to checklists              |
| /checklist/subscriptions               | GET  | Get the checklists the user is subscribed |


### Notes

Use dev tunnels to communicate .net maui androd + .net minimal api locally.

Don't forget to set url from dev tunnel in the BaseAddress for the HttpClient in the .net maui.

[Create and host a dev tunnel](https://learn.microsoft.com/en-us/azure/developer/dev-tunnels/get-started?tabs=windows)
