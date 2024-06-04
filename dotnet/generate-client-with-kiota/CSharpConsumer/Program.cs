using CSharpConsumer;
using CSharpConsumer.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

Console.WriteLine("Hello, World!");

var authProvider = new AnonymousAuthenticationProvider();
var adapter = new HttpClientRequestAdapter(authProvider);
var client = new Client(adapter);
adapter.BaseUrl = "http://localhost:5107";

var createdFoo = await client.Foos.PostAsync(new Foo { Bar = "Baz" });
Console.WriteLine($"New ID: {createdFoo?.Id}");

