# TestableHttp

The purpose of this library is to provide an easily-testable abstraction for HTTP that does a good job of representing an actual HTTP request.

The code in this library originally came from `Emmersion.Http`, which had the same goal as this library. 
Unfortunately, that library has been removed from the nuget.org repository.

## Why?

Unfortunately, the built-in `System.Net.Http.HttpClient` class of the .NET Core library does some really annoying things like:
* Requiring special handling of certain headers, such as `accept` and `content-type`
* Dividing response headers into `.Headers` and `.Content.Headers`
* Doing special cookie handling by default
* Making it difficult to specify a timeout on a per-request basis

_(But at least it is much better than the old .NET Framework http requests which would throw exceptions on 4xx and 5xx responses...)_

## Configuration

Call `TestableHttp.DependencyInjectionConfig.ConfigureServices(services);` to configure DI.
This will make the `IHttpClient` available as a singleton because Microsoft recommends
reusing the underlying `System.Net.Http.HttpClient` for connection pooling.
The default `HttpCLientOptions` are used.

If you have a need to have separate instances of the `TestableHttp.HttpClient`,
or if you wish to provide specific `HttpClientOptions`,
you can create a separate instance or override the DI registration.

## Usage

A very simple GET requests can be performed like this:
```csharp
var request = new HttpRequest
{
    Url = "https://example.com/foo/bar"
};
var response = httpClient.Execute(request);
```

Here is a more involved POST example:
```csharp
public string PostFooBar(string bearerToken, string postData)
{
    var request = new HttpRequest
    {
        Method = HttpMethod.POST,
        Url = "https://example.com/foo/bar",
        Headers = new HttpHeaders()
            .Add("Authorization", $"Bearer {bearerToken}")
            .Add("Content-Type", "text/plain"),
        Body = postData
    };
    var response = httpClient.Execute(request);
    return response.Body;
}
```

The client also exposes a non-blocking `ExecuteAsync` method.

If the request times out, an `HttpTimeoutException` will be thrown.

`StreamHttpRequest` exists if you wish to send a request with the body as a stream.

If you want to receive the response content as a stream rather than a string, use `ExecuteWithStreamResponseAsync()`.

### Extension Methods

There are a few extension methods provided for convenience. For `HttpRequest`:
* `AddBasicAuthentication` - sets the `Authorization: Basic ...` header and base64 encodes the given username:password.
* `AddJsonBody` - sets the `Content-Type: application/json` header and serializes the provided object into the body as JSON.
* `AddFormUrlEncodedBody` - sets the `Content-Type: application/x-www-form-urlencoded` header and encodes the provided data into the body (just as web browsers do for posting forms).

The `HttpResponse` object has:
* `DeserializeJsonBody` - deserializes the response body from JSON into the type you provide.


## Version History

### 1.0
- Initial conversion from `Emmersion.Http` to `TestableHttp`