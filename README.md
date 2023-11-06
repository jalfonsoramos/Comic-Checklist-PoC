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
- As an end-user I want to viwew a checklist and his issues I'm suscribed.
- As an end-user I want to mark an issue as readed from a checklist I'm subscribed.

### Api Doc

Login Endpoints

| endpoint                   | verb      | description                                             |
| ---                        | ---       | ---                                                     |
| /token                     | POST      | Get an access token as Admin or EndUser*                | 

Checklist Endpoints

| endpoint                   | verb      | description                                             |
| ---                        | ---       | ---                                                     |
| /admin/checklist           | POST      | As administrator create new checklist                   |
| /admin/checklist/{id}      | PUT       | As administrator modify checklist                       |
| /admin/checklist/{id}      | GET       | As administrator get single checklist                   |
| /admin/checklist?name&page | GET       | As administrator get checklists                         |
