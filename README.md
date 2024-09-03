# eShop Backend (.NET) Documentation

This document provides an overview and basic instructions for running the eShop backend services. The backend is implemented using .NET, with multiple microservices for different parts of the application, including catalog, order, and basket services. These services are managed using Docker and integrated with IdentityServer for authentication and authorization.

## Services Overview

- **Catalog API**: Handles operations related to product catalog.
- **Order API**: Manages orders and order processing.
- **Basket API**: Manages shopping baskets for users.
- **IdentityServer**: Provides authentication and authorization.
- **Nginx**: Serves as a reverse proxy.
- **PostgreSQL**: Relational database for storing data.
- **Redis**: In-memory data structure store for caching.
- **PgAdmin4**: PostgreSQL database management tool.

## Prerequisites

- Docker and Docker Compose installed
- .NET SDK installed (for local development)

## Running the Application

### Setup Hosts

Add the following entries to your `/etc/hosts` file to map the custom hostname:

```plaintext
127.0.0.1 www.alevelwebsite.com
0.0.0.0 www.alevelwebsite.com
192.168.0.4 www.alevelwebsite.com
```

### Using Docker Compose

To start all services, use Docker Compose with the provided `docker-compose.yml` file:

```bash
docker-compose up --build
```

This command will build and start all services defined in the `docker-compose.yml` file.

### Services Configuration

Each service is configured with environment variables for development. Key environment variables include:

- `ASPNETCORE_ENVIRONMENT`: Set to `Development` for all services.
- `ASPNETCORE_URLS`: URL where each service listens, e.g., `http://+:5001`.
- `ConnectionString`: PostgreSQL connection string for each service.
- `Authorization__Authority`: URL for the IdentityServer.
- `PathBase`: Base path URL for each service.

### Ports

- **Catalog API**: `5001`
- **Order API**: `5005`
- **Basket API**: `5004`
- **IdentityServer**: `5003`
- **PgAdmin4**: `8001`
- **PostgreSQL**: `5433`
- **Redis**: `6380`
- **Nginx**: `80` (HTTP)

## Authentication and Authorization

IdentityServer is configured to provide secure authentication and authorization. The application uses several scopes to manage access:

### Identity Resources

- `openid`: Standard OpenID Connect scope.
- `profile`: Standard OpenID Connect profile scope.

### API Resources and Scopes

- **Host**: `"www.alevelwebsite.com:3000"` with scope `"mvc"`.
- **Catalog API**: `"catalog"` with scopes:
    - `catalog.catalogItem`
    - `catalog.catalogBrand`
    - `catalog.catalogType`
    - `catalog.catalogGender`
    - `catalog.catalogSize`
- **Basket API**: `"basket"` with scope `"basket"`.
- **Order API**: `"order"` with scope `"order"`.

### Clients

- **MVC PKCE Client**: Secure client using PKCE for authentication.
- **Catalog, Basket, and Order Swagger UIs**: Clients for interacting with APIs via Swagger UI with implicit grant type.

## API Documentation

Swagger UI is available for each REST API service to explore and test endpoints:

- **Catalog API**: Accessible at `http://www.alevelwebsite.com:5001/swagger`
- **Order API**: Accessible at `http://www.alevelwebsite.com:5005/swagger`
- **Basket API**: Accessible at `http://www.alevelwebsite.com:5004/swagger`

Each Swagger UI provides detailed information about the API endpoints, request and response models, and allows interactive testing.