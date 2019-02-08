# json-rest-client

Implements methods to build REST queries with JSON as parameter and result.
Supports URI and query string parameters.

## Quick start examples

```c#
using Binateq.JsonRestClient;

class Resource
{
	public int Id { get; set; }

	public string Name { get; set; }
}

var httpClient = new HttpClient();
var baseUri = new Uri("https://api.domain.tld");
var jsonRestClient = new JsonRestClient(httpClient, baseUri);

var resourceId = 100;

var resource1 = await jsonRestClient.GetAsync<Resource>($"v1/resources/{resourceId}");

var resources2 = await jsonRestClient.GetAsync<Resource[]>($"v1/resources");

var resource3 = await jsonRestClient.PostAsync<Resource>($"v1/resources",
    new Resource { Name = "foo" });

var resource4 = await jsonRestClient.PutAsync<Resource>($"v1/resources/{resource3.Id}",
    new Resource { Name = "bar" });
```

## `IHttpClientFactory`

Since the version 1.3.0 the class `JsonRestClient` has constructor with [`IHttpClientFactory`](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.ihttpclientfactory?view=aspnetcore-2.1)
instead of `HttpClient`.

## Extension methods for `HttpClient`

Since the version 1.4.0 the packet implements extension methods for `HttpClient`:

```c#
var httpClient = new HttpClient();
var baseUri = new Uri("https://api.domain.tld");

var resourceId = 100;

var resource1 = await httpClient.GetAsync<Resource>(baseUri, $"v1/resources/{resourceId}");
```

Since the version 1.5.0 extension method can use `HttpClient.BaseAddress` as `baseUri`:

```c#
var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://api.domain.tld");

var resourceId = 100;

var resource1 = await httpClient.GetAsync<Resource>($"v1/resources/{resourceId}");
```

## Ambient context for extension methods

Extension methods of `HttpClient` uses settings from the static property `JsonRestClientSettings.Default`.
You can set them at the start of your program:

```c#
JsonRestClientSettings.Default.ContractResolver = new CamelCasePropertyNamesContractResolver();
JsonRestClientSettings.Default.Formatting = Formatting.Indented;
```

## Query string parameters

```c#
var resourses5 = await jsonRestClient.GetAsync<Resource[]>($"v1/resources",
    new Dictionary<string, object>
    {
        { "from", new DateTime(2018, 04, 17, 11, 37, 00) },
        { "to", null },
        { "starts-with", "" },
        { "ends-with", "bar" },
    });
```

The `JsonRestClient` skips `null` values, but doesn't skip empty strings. Result URI will be:

> `https://api.domain.tld/v1/resources?from=2018-04-17T11:37:00&starts-with=&ends-with=bar`

Also you can use enumerables of primitive types as parameters.

```c#
var resourses6 = await jsonRestClient.GetAsync<Resource[]>($"v1/resources",
    new Dictionary<string, object>
    {
        { "ids", new [] { 1, 2, 3 } },
    });
```

Result URI will be:

> `https://api.domain.tld/v1/resources?ids=1&ids=2&ids=3`

## Cancellation tokens

```c#
var resourses7 = await jsonRestClient.GetAsync<Resource[]>($"v1/resources",
    new Dictionary<string, object>
    {
        { "ids", new [] { 1, 2, 3 } },
    }, CancellationToken.None);
```

## HTTP statuses

### 404 Not Found

There are methods `GetOrDefaultAsync` to process 404 status:

```c#
var resource8 = await jsonRestClient.GetOrDefaultAsync<Resource>($"v1/resources/{resourceId}");
```

This method returns `default(Resource)` i. e. `null` when server sends HTTP status 404.

### 4xx, 5xx

Every method throws `JsonRestException` when server sends status other than 2xx.

```c#
try
{
    var resourses9 = await jsonRestClient.GetAsync<Resource[]>($"v1/resources",
        new Dictionary<string, object>
        {
            { "ids", new [] { 1, 2, 3 } },
        }, CancellationToken.None);
}
catch (JsonRestException exception)
{
    Console.WriteLine($"{exception.StatusCode}, {exception.Uri}, {exception.RequestContent}, {excpetion.ResponseContent}");

    throw;
}
```

`StatusCode` is the [HttpStatusCode](https://msdn.microsoft.com/en-us/library/system.net.httpstatuscode(v=vs.110).aspx)
enumeration.

`ResponseContent` is the string representation of the HTTP response content.

Methods without result like `PutAsync` (not `PutAsync<T>`) has `Foget` form to ignore statuses:

```c#
await jsonRestClient.PutAndForgetAsync($"v1/resources/{resource3.Id}", new Resource { Name = "bar" });
```