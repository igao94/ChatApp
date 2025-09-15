# ChatApp

ChatApp is a RESTful Web API built with **ASP.NET Core 8.0**, following **Clean Architecture** principles. The API allows users to send messages to each other and includes modern chat features such as authentication, authorization, and admin tools.

---

## Technologies

- ASP.NET Core 8.0 Web API  
- JWT Bearer authentication  
- Clean Architecture  
- Repository Pattern with Unit of Work and Generic Repository for common operations  
- Cursor pagination for efficient data retrieval  
- Custom password hashing and salting (no Microsoft Identity)  
- Extension methods for manual DTO mapping  
- **FluentValidation** for robust input validation  
- Initially developed with **MS SQL Server**, now configurable with **In-Memory database** for easier testing  

---

## Features

### For Users
- Registration, login, and JWT authentication  
- Sending messages to other users  
- Editing messages within a limited time after sending  
- Marking messages as read/unread  
- Last seen functionality  
- Searching for messages and users  
- Deactivating and reactivating user profiles

### For Admins
- Viewing all users, including deactivated ones  
- Default admin credentials for testing:
  - **Email:** admin@example.com  
  - **Password:** Pa$$w0rd  

### Messages
- Inbox and Outbox filtering  
- Cursor pagination for efficient chat history retrieval

### Tools
- **Postman Collection** included for all API endpoints to easily test and interact with the API

---

## Installation & Running

1. Clone the repository:
   ```bash
   git clone https://github.com/username/MiniChatApp.git
