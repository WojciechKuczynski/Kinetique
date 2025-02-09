# Project

**Kinetique** is a pet project made intentionally in microservice architecture to manage appointments for patients in small medical clinic. 
Initially this is backend only system without UI.

## Domain descritpion

Application allows recepcionist to simply manage appointments of patients for doctors and with help of system to finish appointments and get simplify process of assigning medical procedures for doctors, preventing from making mistakes and speeding up process.
It also integrates with Goverment systems to send finished appointments with all documentation.

## Technology

Application initially is made with ASP.NET with .NET Core with usage of RabbitMQ as message broker and postgresSQL as database.
Everything is dockerized in docker.

### Future plans

Future plans are to use Kubernetes to manage docker images and make Frontend in React.

## Discovery process

Discovery started with interview with person who is currently using such system.
With simple division to different areas of problem.
![image](https://github.com/user-attachments/assets/0a3dc3a9-18fc-4828-bf72-76401cdd673c)

With more questions and later design some basic microservices were destilled with basic functionalities that will be extended later.

![image](https://github.com/user-attachments/assets/e6b66211-8d61-4b37-870c-13ed91d54666)


## Implementation

With implementation we can already see that coupling between Appointment and Schedule service is getting intense. It might lead us to decision to maybe merge both services into one to lower some latency. But because goal of this project is to generate as much traffic on messageBroker I will leave it as it is.

![image](https://github.com/user-attachments/assets/3cd29a62-c248-4d87-a97a-e48dbc4da559)

