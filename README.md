# Hydra Customers API
Api for customers

![alt text](https://github.com/AlexandreYembo/hydra-customers/blob/main/Customer_Architecture.png)

### Architecture

#### Hydra.Customers.Application

Implement the CQRS design architecture

1. Commands and Commands Handlers (change the entity status on database)

2. Events and Event Handlers (represent the action that happened)

3. Queries (use to read the data from database readonly)

#### Hydra.Customers.Infrastructure

1. EF context

2. Repositories (concreate implementation)

3. Mappings

4. Migrations

5. Services (for intergration propose)

#### Hydra.Customers.Domain

Represent the DDD architecture

1. Models (Aggregrations, entities and value object).

2. Interfaces (Repository)
