# Hospital Management System

## 📋 Project Description

The **Hospital Management System (HMS)** is a comprehensive, role-based web application developed in **ASP.NET Web Forms (VB.NET)** with a **SQL Server** backend. It is designed to optimize hospital operations by integrating key functionalities such as patient records, admissions, billing, and clinical workflows.

This repository focuses on the **Doctor Dashboard Module** – a secure, personalized interface enabling medical professionals to manage their daily schedules and review key patient information in real-time. This feature improves clinical efficiency, minimizes administrative workload, and supports patient-centered care delivery.

---

## 🛠️ Technologies Used

- **Frontend**: ASP.NET Web Forms (VB.NET)
- **Backend**: SQL Server (T-SQL, Stored Procedures)
- **Authentication**: Role-based Access Control (RBAC)
- **Architecture**: 3-Tier Architecture (UI – Business Logic – Data Access)

---

## 🚀 Features of the Doctor Dashboard

- 🔐 **Secure Login** for authenticated doctors
- 📅 **View Upcoming and Past Appointments**
  - Patient name
  - Appointment time & date
  - Room number
  - Appointment status
- 🗃️ **Dynamic Data Fetching** from a normalized SQL Server database
- ⚙️ **Role-Specific Data Access** to ensure confidentiality and HIPAA compliance
- 📈 **Future Scalability**: Design ready for integrating:
  - Test result access
  - Patient history summaries
  - Feedback forms

---

## 🧱 Database Schema Overview (Simplified)

### `Doctors`
| Column         | Type        |
|----------------|-------------|
| DoctorID       | INT (PK)    |
| FullName       | VARCHAR     |
| Specialization | VARCHAR     |
| Email          | VARCHAR     |
| PasswordHash   | VARCHAR     |

### `Appointments`
| Column         | Type        |
|----------------|-------------|
| AppointmentID  | INT (PK)    |
| DoctorID       | INT (FK)    |
| PatientID      | INT (FK)    |
| AppointmentDate| DATETIME    |
| RoomNumber     | VARCHAR     |
| Status         | VARCHAR     |

### `Patients`
| Column         | Type        |
|----------------|-------------|
| PatientID      | INT (PK)    |
| FullName       | VARCHAR     |
| DOB            | DATE        |
| ContactInfo    | VARCHAR     |

---

## 🔒 Access Control

- **Doctor Role**: Can access their own appointments only
- **Admin Role**: Full access to all records (outside the scope of this module)

Role validation is enforced via server-side checks and SQL queries using session-based role verification.

---

## 📷 UI Snapshot (Description)

The dashboard includes:
- A **navigation panel** with links to appointments, profile, and logout
- A **main content area** listing upcoming and past appointments
- Status indicators (e.g., *Scheduled*, *Completed*, *Cancelled*)

---

## 📁 Folder Structure

```plaintext
/DoctorDashboard  
│  
├── /App_Code/  
│   └── DBAccess.vb           # Data access logic  
├── /Doctor/  
│   ├── Dashboard.aspx        # Frontend view  
│   └── Dashboard.aspx.vb     # Code-behind logic  
├── /SQL/  
│   └── schema.sql            # SQL schema and sample data  
├── Web.config                # Authentication and DB settings  
└── README.md                 # Project documentation

---

## 📌 Future Enhancements

- 📄 Integration with lab test modules
- 📝 Patient medical history summaries
- ⭐ Patient-doctor feedback and rating system
- 🧠 AI-assisted appointment prioritization

---

## 🧑‍💻 How to Run

1. Clone the repository.
2. Set up the SQL Server database using `schema.sql`.
3. Open the solution in **Visual Studio**.
4. Configure `Web.config` with your database credentials.
5. Run the project in your local IIS or development server.
