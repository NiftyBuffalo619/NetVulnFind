using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace NetVulnFind
{
    class APIResponse
    {
        public List<WebCam> WebCams { get; set; }
        public static void WriteResults()
        {
            APIResponse WebCams = new APIResponse();
            AnsiConsole.Markup("Writing Results");
            foreach (WebCam cam in WebCams.WebCams)
            {
                AnsiConsole.MarkupLine($"{cam.getIP()}");
            }
        }
    }

    public class WebCam
    {
        public string IP { get; set; }

        public WebCam(string ip)
        {
            this.IP = ip;
        }

        public string getIP()
        {
            return IP;
        }
    }
}
