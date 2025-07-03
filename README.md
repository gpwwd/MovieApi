# Movie Recommendation System with AI Integration

A modern .NET Web API that leverages AI capabilities to provide personalized movie recommendations based on user preferences. The system demonstrates integration with external AI services and implements clean architecture principles.

## Architecture & Technical Highlights

- **Clean Architecture Implementation**
  - Structured in layers (Domain, Application, Infrastructure, Web API)
  - Clear separation of concerns with SOLID principles
  - Domain-driven design approach for movie and user entities
  - Repository pattern for data access abstraction

- **API & Authentication**
  - RESTful API built with ASP.NET Core
  - JWT authentication for secure endpoints
  - Global error handling middleware
  - Swagger documentation for API endpoints

- **Database & Data Access**
  - Entity Framework Core with SQLite
  - Code-first approach with migrations
  - Efficient query optimization
  - Repository pattern implementation

- **AI Integration**
  - OpenRouter API integration for AI-powered recommendations
  - Structured prompt engineering for consistent responses
  - JSON-based communication protocol
  - Error handling and response validation

- **Features**
  - Personalized movie recommendations based on user favorites
  - User preference analysis using AI
  - Genre-based filtering
  - User profile management

## Technology Stack
- Backend: .NET 8, ASP.NET Core
- Database: SQLite, Entity Framework Core
- Authentication: JWT Bearer tokens
- AI Services: OpenRouter API (LLM integration)
- Documentation: Swagger/OpenAPI