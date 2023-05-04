Victorian Plumbing E-Commerce Order API

This is a simple .NET API endpoint that can receive some JSON representing a basic e-commerce order and save it into a SQL database.

The request body should be in JSON format and should contain the following fields:

{ "customerName": "Emma", "items": [ { "productId": "67890", "quantity": 2, "cost": 10.99 }, { "productId": "54321", "quantity": 1, "cost": 5.99 } ] }

This project was created by Emma Billington.
