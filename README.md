# Distillery-Comic-Checklist

Login Endpoints

| endpoint                   | verb      | description                                             |
| ---                        | ---       | ---                                                     |
| /token                     | POST      | Get an access token as Admin or EndUser                 |


Checklist Endpoints

| endpoint                   | verb      | description                                             |
| ---                        | ---       | ---                                                     |
| /admin/checklist           | POST      | As administrator create new checklist                   |
| /admin/checklist/{id}      | PUT       | As administrator modify checklist                       |
| /admin/checklist/{id}      | DELETE    | As administrator delete (soft) checklist (out of scope) |
| /admin/checklist/{id}      | GET       | As administrator get single checklist                   |
| /admin/checklist?name&page | GET       | As administrator get checklists                         |
