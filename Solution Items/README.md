# Introduction 
This project have a mission of handler all related with Items in one Inventory.
It was in charged for me from Goal System
I have protected this API with one Basic authorization (ApiKey) [5988a7f0-b8b6-4226-989d-84145c46cadb]. In the appsettings we can see this configuration

# Getting Started
TODO: Guide users through getting your code up and running on their own system. In this section you can talk about:
1.	Installation process
	Set as startup project: GoalSystem.Inventory.Api, snd run app with VisualStudio and IIS express
2.	Nuget's dependencies
	- Serilog in order to handler all logs of tha Application. This los will be stores in one file (inventoryapi_logYYYYMMdd)
	  placed in the folder c://Logs. We can configure this in appsettings.json
	- Automapper in order to handler all mapping from Entities to DTO's and reverse
	- FluentValidation to handle all the validation rules of the DTOs that come from the request
	- Swagger in order to handler the UI of the API for the Application   

# Build and Test
Compile the App and run Test (from explorer of Tests, run all) 
If we want launch the application API with swagger we can add the ApiKey Secret in the UI of swagger.
This secret key we can get from the appsettings, and we change. Acually the secret key is: "5988a7f0-b8b6-4226-989d-84145c46cadb"
and the name og ApiKey is: "Goal.System". If we want test this API from others places we must add in the headers
of request this information. In other way, the Application API will respond with code 401 -> "Unauthorized"
