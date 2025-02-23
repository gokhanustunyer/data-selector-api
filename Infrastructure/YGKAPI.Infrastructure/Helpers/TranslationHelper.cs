using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace YGKAPI.Infrastructure.Helpers
{
    public static class TranslationHelper
    {
        public static string GetErrorMessageByName(string exceptionName, string lang)
        {
            string baseDirectory = Environment.CurrentDirectory.ToString(); ;
            string translationFilePath = Path.Combine(baseDirectory, "wwwroot", "Locales", lang.ToUpper(), "translation.json");
            Dictionary<string, string> jsonData = LoadJson(translationFilePath);
            jsonData.TryGetValue(exceptionName, out string value);
            return (value != null) ? value : exceptionName;
        }

        public static Dictionary<string, string> LoadJson(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
        }
    }
}