using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetVulnFind
{
    class ShodanReponse
    {
        public List<WebCam> WebCams { get; set; }
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
