# json-rest-extensions

Implements methods to build REST queries with JSON as parameter and result.
Supports URI and query string parameters.

## Quick start examples

```c#
using Binateq.JsonRestExtensions;

class Resource
{
	public int Id { get; set; }

	public string Name { get; set; }
}

var baseUri = new Uri("https://api.domain.tld");
var httpClient = new HttpClient();

var resourceId = 100;

var resource1 = await httpClient.GetAsync<Resource>(baseUri, $"v1/resources/{resourceId}");

var resources2 = await httpClient.GetAsync<Resource[]>(baseUri, $"v1/resources");

var resource3 = await httpClient.PostAsync<Resource>(baseUri, $"v1/resources", new Resource
                {
				    Name = "foo",
				});

var resource4 = await httpClient.PutAsync<Resource>($"https://api.domain.tld/v1/resources/{resource3.Id}", new Resource
                {
				    Name = "bar",
				});
```

## Query string parameters

```c#
var resourses5 = await httpClient.GetAsync<Resource[]>(baseUri, $"v1/resources", new Dictionary<string, object>
                {
				    { "from", new DateTime(2018, 04, 17, 11, 37, 00) },
					{ "to", null },
					{ "starts-with", "" },
					{ "ends-with", "bar" },
				});
```

The library skips `null` values, but doesn't skip empty strings. Result URI is:

> https://api.domain.tld/v1/resources?from=2018-04-17T11:37:00&starts-with=&ends-with=bar

```c#
var resourses6 = await httpClient.GetAsync<Resource[]>(baseUri, $"v1/resources", new Dictionary<string, object>
                {
				    { "ids", new [] { 1, 2, 3, 4, 5, 6, 7 } },
				});
```

Result URI is:

> https://api.domain.tld/v1/resources?ids=1%2c2%2c3%2c3%2c4%2c5%2c6%2c7

Here `%2c` is the URL-encoded comma (`,`) character. You should use [value provider](https://www.strathweb.com/2017/07/customizing-query-string-parameter-binding-in-asp-net-core-mvc/)

## Cancellation tokens

