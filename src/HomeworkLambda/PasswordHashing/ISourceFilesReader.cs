using System.Collections.Generic;

namespace PasswordHashing 
{
    public interface ISourceFilesReader
    {
        IAsyncEnumerable<SourceFile> GetFiles();
    }
}