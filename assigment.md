Phase 1:
Create an api that exposes the necessary end points for a CRUD of the following data:
- Employees, they should have a name, lastname, employeeId and workflow type (crates or buckets)
- Crew, they contain Employees, and have a name of it's own and an extra attribute to describe a ranch (Name: "Crew1", Ranch: "Rancho1")
The api should store the data in a local sqlite db.

Note: you can use an ORM or execute raw sql queries to insert/query the data

Constrains: 
	- You need to use aspnet.core as the backend technology.
	- the project needs to be uploaded in github (please clone the empty repo provided)

once you're done, share the repo link to submit it for revision.

Phase 2:
Modify the existing endpoints to take a json on their's incoming/outcoming payload
constrain: use the the newtonsoft nuget package.

Phase 3:
Adjust the project to enable CORS for the following domains: 
- https://localhost:7006
- http://localhost:5032
