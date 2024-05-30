# Using OpenAPI path parameters with array values

This sample demonstrates how to use [Parameter Serialization](https://swagger.io/docs/specification/serialization/) in OpenAPI to pass array values in path parameters in both simple and matrix styles.

## Endpoints

Two endpoints have been created in the project:

- `GET /foos/{ids}` - Showcases **matrix** style serialization
  - Values will be parameterized in a matrix with a label
  - Example: `/foos;ids=first,second,third`
  - Useful when you have multiple parameters and need a label designating each
  - Note that your label will be included in the first value

- `GET /bars/{ids}` - Showcases **simple** style serialization
  - Values will be parameterized in a simple comma-separated list
  - Example: `/bars/first,second,third`
  - Useful when you have a single parameter and don't need a label and want the shortest possible URI.

## Running the project

```bash
cd Server
dotnet run
```

## Testing the endpoints

### Swagger UI

Once the project is running, the Swagger UI should be available at [http://localhost:5106/swagger](http://localhost:5106/swagger).

If you dig into either of the endpoints in the explorer, you will see that the `ids` parameter is defined as `array[string]` from the path. You can press "Try it now" and note that the Swagger UI offers a button to add multiple string values in the array.

When executed, the parameters will be serialized in the path according to the style defined in the OpenAPI document.

### cURL

```bash
# Matrix style (foos)
curl -X 'GET' 'http://localhost:5106/foos/;ids=first,second'
    [";ids=first","second"]

# Simple style (bars)
curl -X 'GET' 'http://localhost:5106/bars/first,second'
    ["first","second"]
```

## Considerations

- If you use **matrix** style serialization, your label will be included in the first value.
- You can modify the Swashbuckle configuration to use types other than 'string' such as 'integer'. However, these endpoints are configured to map the serialized values to a string and model binding will not automatically bind these to an `IEnumerable<string | int>` for you.
