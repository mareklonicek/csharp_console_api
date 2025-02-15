using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace httpClient_api;
public class UserService
{
    private readonly HttpClient _client = new HttpClient { BaseAddress = new Uri("https://jsonplaceholder.typicode.com/") };

    public async Task GetUsers()
    {
        var response = await _client.GetStringAsync("users");
        Console.WriteLine(response);
    }

    public async Task GetUser(int id)
    {
        var response = await _client.GetStringAsync($"users/{id}");
        Console.WriteLine(response);
    }

    public async Task CreateUser(User user)
    {
        var json = JsonSerializer.Serialize(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("users", content);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }

    public async Task UpdateUser(int id, User user)
    {
        var json = JsonSerializer.Serialize(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PutAsync($"users/{id}", content);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }

    public async Task DeleteUser(int id)
    {
        var response = await _client.DeleteAsync($"users/{id}");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }
}
