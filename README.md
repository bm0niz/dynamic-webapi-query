# Dynamic Querying WebAPIs

Creating endpoints for every query is no fun. This repo demonstrates how you can implement dynamic querying using LINQ to filter, sort and expand when fetching REST resources.

1. Run the API

```bash
dotnet run --project DynamicQuery.WebApi
```

2. Query the endpoint with your favorite http client (curl examples below):

```bash
# filtering
curl --get "http://localhost:5240/orders" --data-urlencode 'filter=type == "D1"'
curl --get "http://localhost:5240/orders" --data-urlencode 'filter=type.Contains("D") && id != 2'

# sorting
curl --get "http://localhost:5240/orders" --data-urlencode 'order=id desc'
curl --get "http://localhost:5240/orders" --data-urlencode 'order=customer.id, id desc'

# expanding child entities
curl --get "http://localhost:5240/orders" --data-urlencode 'expand=Customer,Lines'
```