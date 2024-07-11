using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Console;

public class Bot
{
    private string token;
    public Bot(string Token) => token = Token;
    public async Task Start()
    {
        Debugger.Debug($"Logging in {token}", "Bot"); 

        var channel_id = "1258227126688153604";

        var data = new Dictionary<string, string>
        {
            { "channel_id", channel_id },
            { "content","mensaje de prueba" }
        };


        using (var client = new HttpClient())
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Add("Authorization", token);
            var response= await client.GetAsync($"https://discord.com/api/v9/users/@me");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                Debugger.Debug($"Logged", "Bot");
            else
                Debugger.Error($"Error > {response.StatusCode}");
        }
    }
}