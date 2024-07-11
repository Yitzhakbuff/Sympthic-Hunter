using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Console;

public class DiscordUtil
{
    
    public static async Task<List<DiscordMessage>> GetMessagesFromChannel(string cId, string token)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", token);
            var r = await client.GetAsync($"https://discord.com/api/v9/channels/{cId}/messages?limit=5");
            var content = await r.Content.ReadAsStringAsync();
            var messages = JsonSerializer.Deserialize<List<DiscordMessage>>(content);
            
            return messages;
        }
    }
}
public class DiscordMessage
{
    public string id {get; set;}
    public string content {get;set;}
}
