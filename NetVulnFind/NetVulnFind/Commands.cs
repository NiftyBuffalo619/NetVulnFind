using System;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;

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
        private const string BaseUrl = "https://search.censys.io/api/";
        private readonly HttpClient httpClient;

        public DetectWebCams()
        {
            httpClient = new HttpClient();
            /*httpClient.DefaultRequestHeaders.Add("UID", LoadConfig.API_KEY);
            httpClient.DefaultRequestHeaders.Add("SECRET", LoadConfig.APP_SECRET);*/
        }

        public override int Execute([NotNull] CommandContext context, [NotNull] DetectWebCamSettings settings)
        {
            string selectedCountry = settings.CountryCode ?? PromptForCountry();
            try
            {
                APIResponse response = SearchWebCamsAsync(selectedCountry).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                AnsiConsole.WriteException(e);
            }
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

        public async Task<APIResponse> SearchWebCamsAsync(string country)
        {
            string query = "\"WebCamXP 5\"";
            string url = $"https://search.censys.io/api/v2/hosts/search?q=services.http.response.html_title:{Uri.EscapeDataString(query)}";
            string credentials = $"{LoadConfig.API_KEY}:{LoadConfig.APP_SECRET}";
            byte[] credentialsBytes = Encoding.UTF8.GetBytes(credentials);
            string credentialsBase64 = Convert.ToBase64String(credentialsBytes);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentialsBase64);
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(content);
                AnsiConsole.MarkupLine($":green_circle:[green1]{apiResponse.Code} OK[/]:check_mark:");
                AnsiConsole.MarkupLine($"[green1]Total: {apiResponse.Result.Total}[/]");

                var table = new Table();
                table.AddColumn("IP");
                table.AddColumn("Service Name").Centered();
                table.AddColumn("Country").Centered();
                table.AddColumn("City").Centered();
                foreach (Hit hit in apiResponse.Result.Hits)
                {
                    /*var columns = new List<Text>()
                    {
                        new Text($"IP: {hit.IP} Location: {hit.Location.CountryCode} {hit.Location.Country} {hit.Location.City}"),
                    };
                    AnsiConsole.Write(new Columns(columns));*/
                    //Console.WriteLine($"IP: {hit.IP} Location: {hit.Location.CountryCode} {hit.Location.Country} {hit.Location.City}");
                    foreach (Service service in hit.Services)
                    {
                        table.AddRow($"[link]{hit.IP}:{service.Port}[/]", $"{service.ExtendedServiceName}", $"{hit.Location.Country}", $"{hit.Location.City}");
                    }
                }
                AnsiConsole.Write(table);
               // Console.WriteLine(content);
                return JsonConvert.DeserializeObject<APIResponse>(content);
            }

            AnsiConsole.MarkupLine(":cross_mark: [red1]Something went wrong[/] :cross_mark:");
            throw new Exception($"API request failed with status code {response.StatusCode}");
        }
    }
}