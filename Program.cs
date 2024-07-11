using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static System.Console;
using System.Linq;

public class Program
{
    public static Dictionary<string, string> cache = new Dictionary<string, string>();
    public static async Task Main()
    {
        await Start();
    }
    public static async Task Start()
    {
        Clear();
        LoadCache();

        Title();
        WriteLine(@"Selecciona una opcion:

            1. Iniciar bot
            2. Ajustes
            3. Test
            0. Salir");
        Write(">> "); 
        var s = int.Parse(ReadLine());
        switch (s)
        {
            case 1:
                var bot = new Bot(cache["token"]);
                await bot.Start();
                break;
            case 2:
                await ConfigCache();
                break;
            case 3:
                    WriteLine("Escribe el ID del canal");
                    Write(">>");
                    var messages = await DiscordUtil.GetMessagesFromChannel(ReadLine(), cache["token"]);

                    foreach (var message in messages)
                    {
                        WriteLine($"Mensaje: {message.content}  | ID: {message.id}");   
                    }
                break;
        }
    }
    public static async Task ConfigCache()
    {
        Clear();
        LoadCache();
        WriteLine("Selecciona escribiendo la propiedad que quieres ajustar: ");
        int i = 0;
        foreach (var prop in cache)
        {
            i++;
            WriteLine($"\t{i}. {prop.Key} = {prop.Value}");
        }
        WriteLine("\n\t0. Regresar");
        Write(">> ");
        int option;
        while(!int.TryParse(ReadLine(), out option) || option < 0 || option > cache.Count)
        {
            WriteLine("Opcion invalida, intente de nuevo.");
        }
        if (option==0)
            await Start();
        var selectedKey = cache.Keys.ElementAt(option - 1);
        Write($"Escriba el nuevo valor para {selectedKey}: ");
        cache[selectedKey] = ReadLine();
        SaveCache();
        await ConfigCache();
    }
    public static void LoadCache()
    {
        try{
            //lo de regex use chat gpt
            var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config"));
            Regex regex = new Regex(@"^\s*(\w+)\s*=\s*""([^""]*)""\s*$");
            foreach (var line in lines)
            {
                Match match = regex.Match(line);
                if (match.Success)
                {
                    string key = match.Groups[1].Value;
                    string value = match.Groups[2].Value;
                    cache[key] = value;
                }
            }
        }
        catch (Exception e){
            WriteLine(e.Message);
        }
    }
    public static void SaveCache()
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config")))
            {
                foreach (var kvp in cache)
                {
                    sw.WriteLine($"{kvp.Key}=\"{kvp.Value}\"");
                }
            }
        }
        catch (Exception e)
        {
            WriteLine($"Error al guardar el caché: {e.Message}");
        }
    }

    public static void Title()
    {
        var titleLines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "title.txt"));
        int ci=252;
        foreach (var line in titleLines)
        {
            ci--;
            WriteLine($"\x1b[38;5;{ci}m{line}\x1b[37m");
        }
    }
}