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
        public int Code { get; set; }
        public string Status { get; set; }
        public Result Result { get; set; }
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
    public class Result
    {
        public string Query { get; set; }
        public int Total { get; set; }
        public int Duration { get; set; }
        public List<Hit> Hits { get; set; }
    }

    public class Hit
    {
        public List<Service> Services { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public string IP { get; set; }
        public AutonomousSystem AutonomousSystem { get; set; }
        public Location Location { get; set; }
        public DNS DNS { get; set; }
    }

    public class Service
    {
        public string ExtendedServiceName { get; set; }
        public int Port { get; set; }
        public string ServiceName { get; set; }
        public string Certificate { get; set; }
        public string TransportProtocol { get; set; }
    }

    public class AutonomousSystem
    {
        public string Description { get; set; }
        public string CountryCode { get; set; }
        public int ASN { get; set; }
        public string BGPPrefix { get; set; }
        public string Name { get; set; }
    }

    public class Location
    {
        public string Continent { get; set; }
        public string PostalCode { get; set; }
        public string TimeZone { get; set; }
        public string City { get; set; }
        public Coordinates Coordinates { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
    }

    public class Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class DNS
    {
        public ReverseDNS ReverseDNS { get; set; }
    }

    public class ReverseDNS
    {
        public List<string> Names { get; set; }
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