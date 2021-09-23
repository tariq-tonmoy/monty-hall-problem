# Introduction 
This is a simulation to prove the paradox that is the Monty-Hall problem 

# Environment Requirement
.Net 5 SDk needs to be installed to run the solution in Visual Studio 2019. Moreover, Docker can be used to run the backend services. 

# Backend Microservices
Here is the list of all microservices and their relative locations which are necessary for the client to run the simulations.
* *CommandWebHost*: src/CommandWebHost/CommandWebHost.csproj
* *CommandWorkerHost*: src/CommandWorkerHost/CommandWorkerHost.csproj
* *EventWorkerHost*: src/EventWorkerHost/EventWorkerHost.csproj
* *QueryWebHost*: src/QueryWebHost/QueryWebHost.csproj
* *NotificationHost*: src/External/External.NotificationHost/External.NotificationHost.csproj

# Client Application
The client application is a WPF core application to visualize simulation results in real time and to browse previous results stored in *Sqlite* database. 
* *FrontEndClient*: src/ClientSide/FrontEndClient/FrontEndClient.csproj

To start using the client, please select the environment by selecting either VisualStudio or Docker from the environment dropdown as shown in the image.

![Environment selection](https://i.ibb.co/c3FDfzs/Screenshot-2021-09-24-010146.png)


# Run Project:
* *Docker*: Run docker-compose.yml file
* *Visual Studio*: Run MontyHallProblemSimulation.sln file