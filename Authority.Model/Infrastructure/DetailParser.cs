using Authority.Model.Domain.Entity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Authority.Model.Infrastructure
{
    internal static class DetailParser
    {
        private static readonly string jsonPath_ = @"..\..\..\details.json";

        public static FlagDetail GetDetail(string programName)
        {
            var json = File.ReadAllText(jsonPath_);
            var details = JsonConvert.DeserializeObject<List<FlagDetail>>(json);
            foreach (var detail in details)
            {
                if (detail.ProgramName == programName)
                {
                    return detail;
                }
            }
            return null;
        }
    }
}