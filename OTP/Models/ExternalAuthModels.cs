using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OTP.Models
{
    public class ExternalAuthModels
    {
        public string AccountKit_ClientKeyIdentifier { get; set; }
        public string AccountKit_CSRF { get; set; }
        public string AccountKit_API_Version { get; set; }
        public string AccountKit_Locale { get; set; }

        public string ReturnUrl { get; set; }
    }
}