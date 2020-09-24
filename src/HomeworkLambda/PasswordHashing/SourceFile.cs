using System.IO;

namespace PasswordHashing 
{
    public class SourceFile
    {
        public Stream CsvStream { get; }
        public string FileName { get; }

        public SourceFile(Stream csvStream, string fileName)
        {
            CsvStream = csvStream;
            FileName = fileName;
        }
    }
}