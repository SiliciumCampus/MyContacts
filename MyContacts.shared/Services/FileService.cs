using MyContacts.shared.Interfaces;
using System.Diagnostics;

namespace MyContacts.shared.Services;

public class FileService : IFileService
{
    public string GetContentFromFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            else if (!File.Exists(filePath))
            {
                using (FileStream fs = File.Create(filePath)){}
                return null!;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }



    public bool SaveContentToFile(string filePath, string content)
    {
        try
        {
            using var sw = new StreamWriter(filePath);
            sw.WriteLine(content);
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }
}
