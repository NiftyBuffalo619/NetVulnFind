using System;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console;
using Spectre.Console.Cli;

namespace NetVulnFind
{
    class Commands
    {
    }
    class DetectWebCamSettings : CommandSettings
    {
        [CommandOption("-c|--country <CountryCode>")]
        public string CountryCode { get; set; }
    }
    class DetectWebCams : Command<DetectWebCamSettings>
    {
        public override int Execute([NotNull] CommandContext context, [NotNull] DetectWebCamSettings settings)
        {
            string selectedCountry = settings.CountryCode ?? PromptForCountry();
            return 0;
        }

        private string PromptForCountry()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Choose from one of the countries :backhand_index_pointing_down:")
                .PageSize(10)
                .MoreChoicesText("(Move up and down to reveal more fruits)")
                .AddChoices(new[] {
                    "United States of America US",
                    "Federal Republic of Germany (Bundesrepublik Deutschland) DE",
                    "Czech Republic (Česká Republika) CZ",
                })
            );
        }
    }
}
