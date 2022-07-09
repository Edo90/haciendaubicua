namespace haciendaubicua.Utils
{
    public static class Utils
    {
        public static string GetFolderPath(string folderName)
        {
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}{folderName}\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }
    }
}
