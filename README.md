# EduPolicy-Capstone-Project-GSC
# EduPolicy - is a Web-based Policy Management System for Golden Success College Students.

**OVERVIEW**:

The purpose of this project is to address the challenges faced by Golden Success College Management in managing student policy violations. The existing manual process, which relies on logbooks, makes it difficult for administrators to monitor offenses effectively and locate specific records when verifying student compliance during clearance signing. Although proofs of compliance exist, the management struggles to retrieve them efficiently due to the volume of student violations.

EduPolicy aims to resolve these issues by developing a web-based policy management system that automates the monitoring and reporting of student violations. The system provides a secure database for storing records, enables real-time tracking of policy infractions across different school levels, and delivers solid proof of compliance without requiring extensive manual record searching, ensuring a more efficient and reliable clearance process.

# TECH STACK
This project is built using the following technology:
- ASP.NET 4.7 MVC for frontend
- Bootstrap Framework for UI
- jQuery and AJAX to support CRUD Operation
- ASP.NET 4.7 Web API 2 for backend
- MySQL for RDBMS
- Postman for API Testing
- IIS for Application Manual Testing

# Use Case
This project both frontend and backend is currently hosted using Azure Free Tier (F1) and the database is also hosted using FreeDB although limited yet this project is still accessible for demonstration
but with a catch is that you cannot search any students data at the Marshall Interface using the hosted Website URL (https://gscedupolicyfront.azurewebsites.net) since this project has access to the school's
API that returns all the simple data of the students that exist such as (ID No., Fullname) and the API is protected by the Cloudflare and it treats the Azure request as a bot and does not return any data however
you can still use the following credential for testing:                                                                                                                                                        Admin Login                                                                                                                                                                                                  username: superuser_l                                                                                                                                                                                               password: whoami
                                                                                                                                                                                                                                                                                                                                                                                                                                Marshall Tab unique key: iamroot

There are sample violation data that is been recorded in the database that is hosted but you cannot add other violation data since again about the cloudflare issue, however you can still manually add student violation data since this project contains the Backend API using Postman thru parameter or use Params and add the following key (prior_no, studentid, last_name, first_name, middle_name, gender, off_id, u_k) with your preferred value and use this URL (https://gscedupolicyback.azurewebsites.net/api/student/violation_add)

# FEATURES

## Main Page
The main dashboard provides an overview of system activities, allowing administrators and marshals to monitor student violations and sanctions efficiently.

![Main Page](https://github.com/user-attachments/assets/b7c3df5d-74f5-4f36-99ea-ff7e55687f41)

---

## Admin and Marshall Login Page
Both **Admin** and **Marshall** users have dedicated login access to ensure secure and role-based system entry.

### Admin
![Admin Login](https://github.com/user-attachments/assets/5ec653a7-ea06-4f05-8e1f-15f32344a01f)

### Marshall
![Marshall Login](https://github.com/user-attachments/assets/5135f2cf-a59f-4ded-adbb-974b85278733)

---

## Student Monitoring Page
Students do not have a login page; instead, they can track their policy violations and pending obligations using their **unique school identification number**.

![Student Monitoring](https://github.com/user-attachments/assets/9050a3a9-eb0f-4f06-9fd9-1111919e941a)

---

## User Roles
The system operates under **three roles:**  
- **Admin** – Manages overall system data and user roles.  
- **Marshall** – Records violations.  
- **Student** – Monitors their own violation records and obligations.

---

## Admin Side
The **Admin Side** provides a full control panel to manage students, marshals, violations, and sanctions. Below are the main administrative interfaces:

![Admin Dashboard 1](https://github.com/user-attachments/assets/a77b5261-c859-4638-b1eb-e44cfad9593e)
![Admin Dashboard 2](https://github.com/user-attachments/assets/e2c09283-fffe-438b-b55a-9576a3f3d26b)
![Admin Dashboard 3](https://github.com/user-attachments/assets/f7b4eb56-b7a0-4feb-b80c-c3764d7521cf)
![Admin Dashboard 4](https://github.com/user-attachments/assets/7f568dec-8635-4db9-983f-4719a1751cd0)
![Admin Dashboard 5](https://github.com/user-attachments/assets/f6b3b01a-82dc-4ebb-8859-f538f4e1ec0f)
![Admin Dashboard 6](https://github.com/user-attachments/assets/f35896ad-97c8-4dc4-9b21-8cd076a95fdc)


## Marshal Side
The **Marshal Side** provides a full control panel to record violations. Below are the main Marshall interfaces:

![Marshall Dashboard 1](https://github.com/user-attachments/assets/7de29774-a192-4e27-bfaf-8a09896fc7bd)
![Marshall Dashboard 2](https://github.com/user-attachments/assets/ee6db305-c8b9-4a21-a120-b8397b0cf2f4)
![Marshall Dashboard 3](https://github.com/user-attachments/assets/c1ae1a3f-c39c-46e9-8c1d-a1f5481a8238)
![Marshall Dashboard 4](https://github.com/user-attachments/assets/92ff360d-a1ff-45ee-b27f-d2577adc866f)
![Marshall Dashboard 5](https://github.com/user-attachments/assets/d9707970-fe93-4a86-9e98-cf218e437922)

## Student Side
The **Student Side** provides an interface to monitor violation with corresponding sanction. Below are the main Student interface:


![Student Dashboard 1](https://github.com/user-attachments/assets/93fdc2b7-5052-45ec-87ae-aeb744af2d9a)

## Researcher & Developer Information

**Project Manager/Researcher:** Medjie de Padua  
Responsible for conducting surveys and meetings, gathering data, and preparing the research documentation that supported the system’s development.

**Developer:** Dustin Lyle Cuizon  
Responsible for system fullstack development.

**Project Title:** EduPolicy – Web-Based Policy Management System for Golden Success College Students  
**Research Category:** Capstone Project (Academic Year 2024–2025)  

Summary

EduPolicy demonstrates how technology can streamline policy management in an academic environment. By automating monitoring, reporting, and compliance tracking, it helps administrators maintain discipline efficiently while providing students with transparency regarding their obligations.
