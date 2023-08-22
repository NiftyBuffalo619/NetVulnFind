using System;
using Spectre.Console;
using System.Threading.Tasks;
using Spectre.Console.Cli;

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
            AnsiConsole.MarkupLine("[green1]Done[/]" + ":check_mark:");
        }
        static async Task<int> Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "NetVulnFind";
            AnsiConsole.Write(new Markup(@"[bold red1] 
  _   _      ___      __    _       ______ _           _ 
 | \ | |    | \ \    / /   | |     |  ____(_)         | |
 |  \| | ___| |\ \  / /   _| |_ __ | |__   _ _ __   __| |
 | . ` |/ _ \ __\ \/ / | | | | '_ \|  __| | | '_ \ / _` |
 | |\  |  __/ |_ \  /| |_| | | | | | |    | | | | | (_| |
 |_| \_|\___|\__| \/  \__,_|_|_| |_|_|    |_|_| |_|\__,_|
                                                         [/]"));
            AnsiConsole.MarkupLine($"[red1]{Reference.Version} by wannabeNifty[/]");
            _ = LoadConfig.Config(); // Just for Dev purposes 
            await LoadingConfig();
            if (LoadConfig.IsAPIKEYEMPTY())
            {
                AnsiConsole.Prompt(new TextPrompt<string>("Enter your API key:").PromptStyle("red").Secret(null));
            }
            Console.Clear();
            Console.Title = "NetVulnFind";
            AnsiConsole.Write(new Markup(@"[bold red1] 
  _   _      ___      __    _       ______ _           _ 
 | \ | |    | \ \    / /   | |     |  ____(_)         | |
 |  \| | ___| |\ \  / /   _| |_ __ | |__   _ _ __   __| |
 | . ` |/ _ \ __\ \/ / | | | | '_ \|  __| | | '_ \ / _` |
 | |\  |  __/ |_ \  /| |_| | | | | | |    | | | | | (_| |
 |_| \_|\___|\__| \/  \__,_|_|_| |_|_|    |_|_| |_|\__,_|
                                                         [/]"));
            AnsiConsole.MarkupLine($"[red1]{Reference.Version} by wannabeNifty[/]");

            /*bool Continue = true;
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
            while (Continue != false);*/
            var app = new CommandApp();
            app.Configure(config =>
            {
                config.AddCommand<DetectWebCams>("detect-webcams");
            });
            bool Continue = true;
            do
            {
                var Command = AnsiConsole.Ask<string>("Command>");
                switch (Command)
                {
                    case "detect-webcams":
                        await AnsiConsole.Progress()
                        .Columns(new ProgressColumn[]
                        {
                            new SpinnerColumn(),
                        })
                        .StartAsync(async ctx =>
                        {
                            await app.RunAsync(new[] { "detect-webcams", "--country", "CZ" });
                        });
                        break;
                    case "help":
                        var table = new Table();
                        table.Border = TableBorder.Rounded;
                        table.AddColumn("Command");
                        table.AddColumn("Description");
                        table.AddRow("help", "Shows table with commands to help the user.");
                        table.AddRow("detect-webcams", "Looks for webcams :construction: :warning: (Please note not all IP's work you have to try to visit them) :warning: ");
                        table.AddRow("clear", "Cleares the console.");
                        table.AddRow("about :sparkles:", "Some fun fact command");
                        table.AddRow("github", "Gives a link where you can find this repository");
                        AnsiConsole.Write(table);
                        break;
                    case "github":
                        AnsiConsole.WriteLine("\n\n");
                        var GithubTable = new Table();
                        GithubTable.Border = TableBorder.Minimal;
                        GithubTable.AddColumn(":books:Github Repository");
                        GithubTable.AddColumn("The Github repository can be found :backhand_index_pointing_right: [link]https://github.com/NiftyBuffalo619/NetVulnFind[/]");
                        GithubTable.AddRow("If you find this repository helpful or interesting, please consider giving it a [yellow]star[/]:glowing_star:");
                        AnsiConsole.Write(GithubTable);
                        //AnsiConsole.MarkupLine("If you find this repository helpful or interesting, please consider giving it a [yellow]star[/]:glowing_star:");
                        AnsiConsole.WriteLine("\n\n");
                        break;
                    case "clear":
                        Console.Clear();
                    break;
                    case "about":
                        AnsiConsole.MarkupLine("About :sparkles:");
                        AnsiConsole.MarkupLine("");
                        var AboutTable = new Table();
                        AboutTable.Border = TableBorder.Minimal;
                        AboutTable.AddColumn("General Info");
                        AboutTable.AddColumn("NetVulnFind is fun made project by [bold]wannabeNifty[/] made to learn programming and some web vulnerabilities");
                        AnsiConsole.Write(AboutTable);
                        break;
                    case "exit":
                        AnsiConsole.MarkupLine("[red1]Bye![/]");
                        Continue = false;
                        break;
                    default:
                        AnsiConsole.MarkupLine("[red1]Invalid Command[/]");
                        break;
                }
            }
            while (Continue != false);
            return await app.RunAsync(args);
        }
    }
}