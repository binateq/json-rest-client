# json-rest-extensions

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

> https://api.domain.tld/v1/resources?from=2018-04-17T11:37:00&starts-with=&ends-with=bar

```c#
var resourses6 = await jsonRestClient.GetAsync<Resource[]>($"v1/resources",
    new Dictionary<string, object>
    {
        { "ids", new [] { 1, 2, 3 } },
    });
```

Result URI will be:

> https://api.domain.tld/v1/resources?ids=1&ids=2&ids=3

## Cancellation tokens

```c#
var resourses6 = await jsonRestClient.GetAsync<Resource[]>($"v1/resources",
    new Dictionary<string, object>
    {
        { "ids", new [] { 1, 2, 3 } },
    }, CancellationToken.None);
```
