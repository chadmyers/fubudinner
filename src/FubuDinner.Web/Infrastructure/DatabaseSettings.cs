using FubuMVC.Core.Models;

namespace FubuDinner.Web.Infrastructure
{
    public class DatabaseSettings
    {
        [MapWebToPhysicalPath]
        public string DbFilePath { get; set; }
    }
}