using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YGKAPI.Infrastructure.Operations.Statics;

namespace YGKAPI.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainerName, string fileName);
        protected async Task<string> FileRenameAsync(string path, string fileName, HasFile hasFileMethod)
        {
            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);
            string newFileName = NameOperation.CharacterRegulatory(oldName);

            string nameNumber = "";
            int nameCount = 0;
            while (hasFileMethod(path, $"{newFileName}{nameNumber}{extension}"))
            {
                nameCount++;
                nameNumber = $"-{nameCount}";
            }
            newFileName = $"{newFileName}{nameNumber}{extension}";
            return newFileName;
        }
    }
}