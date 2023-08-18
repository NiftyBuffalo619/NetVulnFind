using System;
using Spectre.Console;
using System.Threading.Tasks;

namespace NetVulnFind
{
    class Program
    {
        async static void LoadingConfig()
        {
            // https://spectreconsole.net/live/progress
            await AnsiConsole.Progress()
            /*.Columns(new ProgressColumn[]
            {
                new ProgressBarColumn(),
            })*/
           .StartAsync(async ctx =>
           {
               // Define tasks
               var Message = "[green]Reading Config File[/]";
               var task1 = ctx.AddTask(Message);

               while (!ctx.IsFinished)
                {
                   await LoadConfig.Config();
                   await Task.Delay(2);
                   task1.Increment(1);
                }
           });
            AnsiConsole.MarkupLine("[green]Done[/]:check_mark:");
        }
        static void Main(string[] args)
        {
            Console.Title = "NetVulnFind";
            AnsiConsole.Write(new Markup(@"[bold red] 
  _   _      ___      __    _       ______ _           _ 
 | \ | |    | \ \    / /   | |     |  ____(_)         | |
 |  \| | ___| |\ \  / /   _| |_ __ | |__   _ _ __   __| |
 | . ` |/ _ \ __\ \/ / | | | | '_ \|  __| | | '_ \ / _` |
 | |\  |  __/ |_ \  /| |_| | | | | | |    | | | | | (_| |
 |_| \_|\___|\__| \/  \__,_|_|_| |_|_|    |_|_| |_|\__,_|
                                                         [/]"));
            LoadConfig.Config();
            LoadingConfig();
            
            Console.ReadKey();
        }
    }
}