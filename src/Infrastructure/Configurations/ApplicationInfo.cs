using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class ApplicationInfo
    {
         public string LoginPath { get; set; } = string.Empty;
        public string AppDomain { get; set; } = string.Empty;
        public string AccountConfirmation { get; set; } = string.Empty;
        public string ForgotPassword { get; set; } = string.Empty;
    }
}