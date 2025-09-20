public class LocalBucketService
{
    private readonly string _basePath;

    public LocalBucketService(string basePath)
    {
        _basePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
    }

    // Retorna a URL que o navegador consegue acessar
    public string UploadFile(string fileName, byte[] fileContent, string extension)
    {
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var finalName = $"{fileName}_{timestamp}{extension}";

        var filePath = Path.Combine(_basePath, finalName);
        File.WriteAllBytes(filePath, fileContent);

        // URL relativa para navegador
        return $"/uploads/{finalName}";
    }

    public byte[] GetFile(string fileName)
    {
        var filePath = Path.Combine(_basePath, fileName);
        if (!File.Exists(filePath))
            throw new FileNotFoundException("File not found", fileName);

        return File.ReadAllBytes(filePath);
    }

    public bool DeleteFile(string fileName)
    {
        var filePath = Path.Combine(_basePath, fileName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return true;
        }
        return false;
    }
}
