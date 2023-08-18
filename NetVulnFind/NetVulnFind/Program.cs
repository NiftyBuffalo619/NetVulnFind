using System;
using Spectre.Console;
using System.Threading.Tasks;

namespace NetVulnFind
{
    class Program
    {
        async static Task LoadingConfig()
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
               var Message = "[green1]Reading Config File[/]";
               var task1 = ctx.AddTask(Message);

               while (!ctx.IsFinished)
                {
                   await LoadConfig.Config();
                   await Task.Delay(2);
                   task1.Increment(1);
                }
           });
            AnsiConsole.MarkupLine("[green1]Done[/]:check_mark:");
        }
        static async Task Main(string[] args)
        {
            Console.Title = "NetVulnFind";
            AnsiConsole.Write(new Markup(@"[bold red1] 
  _   _      ___      __    _       ______ _           _ 
 | \ | |    | \ \    / /   | |     |  ____(_)         | |
 |  \| | ___| |\ \  / /   _| |_ __ | |__   _ _ __   __| |
 | . ` |/ _ \ __\ \/ / | | | | '_ \|  __| | | '_ \ / _` |
 | |\  |  __/ |_ \  /| |_| | | | | | |    | | | | | (_| |
 |_| \_|\___|\__| \/  \__,_|_|_| |_|_|    |_|_| |_|\__,_|
                                                         [/]"));
            AnsiConsole.MarkupLine("[red1]v1.0[/]");
            LoadConfig.Config();
            await LoadingConfig();
            bool Continue = true;
            do
            {
                AnsiConsole.Markup("[cyan]$>[/]");
                string arguments = Console.ReadLine().ToLower();
                if (arguments.Length == 0)
                {
                   
                }
                string[] commands = arguments.Split(' ');
                string command = commands[0].ToString();
                Console.WriteLine(command);
                switch (command)
                {
                    case "help":
                        AnsiConsole.MarkupLine("[cyan]Help[/]");
                    break;
                    case "exit":
                        Continue = false;
                    break;
                    default:
                        AnsiConsole.MarkupLine("[red1]Invalid Command[/]");
                    break;
                }
            }
            while (Continue != false);
        }
    }
}