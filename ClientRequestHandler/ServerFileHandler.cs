using System.IO;

namespace ClientRequestHandler
{
    public class ServerFileHandler
    {
        private const string BaseServerDirectory = @"C:\TestServer";
        private const string DirectoryRequestedPage = @"C:\TestServer\directory.html";
        private const string FileNotFoundFallBack = @"C:\TestServer\file_not_found_404.html";
        public static string GetRequestedFilePath(string relativeUrlPath)
        {
            if (relativeUrlPath == "/") return BaseServerDirectory;

            // Windows style path and remove the starting '\' to avoid getting root path
            var relativeWindowsPath = relativeUrlPath.Replace("/", "\\").Substring(1);

            return Path.Combine(BaseServerDirectory, relativeWindowsPath);
        }
        public static void GetFileData(ref string filePath, out byte[] contents, out bool fileFound)
        {
            if (Directory.Exists(filePath) == true)
            {
                filePath = DirectoryRequestedPage;
                fileFound = true;
            }
            else if (File.Exists(filePath) == false)
            {
                filePath = FileNotFoundFallBack;
                fileFound = false;
            }
            else
            {
                fileFound = true;
            }
            contents = ReadFileAsByteArray(filePath);
        }
        private static byte[] ReadFileAsByteArray(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}
