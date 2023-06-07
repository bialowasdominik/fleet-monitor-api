# Fleet Monitor
>Back-end

Part of the system that was the subject of my engineering thesis. 

The system is designed to present current and historical data regarding vehicle trips in a clear manner. It enables profit optimization and ensures the safety of employees through real-time monitoring of vehicle locations.

The back-end of the Fleet Monitor application. It handles communication with the database, the front-end application, and the vehicle's tracking device. Communication with the application is done using the REST architecture and the HTTP protocol. This makes it easy to integrate with external services or, for example, a mobile application.

The application was deployed in an Azure environment using a virtual machine, IIS server, and Azure SQL.

**Technologies used in project:**
- C#
- ASP.NET Core 6.0
- EntityFramework Core
- FluentValidation
- AutoMapper
- Swashbuckle

**The image below presents a list of all endpoints available in the application generated using the Swashbuckle package**

![image](https://github.com/bialowasdominik/fleet-monitor-api/assets/106835786/aeb41334-03ca-4986-9830-52c8bf641468)

**The image below presents the structure of the database generated using the EntityFramework tool**

![image](https://github.com/bialowasdominik/fleet-monitor-api/assets/106835786/aa4051d9-51a0-4cc5-bd56-74dd9e76c36c)

