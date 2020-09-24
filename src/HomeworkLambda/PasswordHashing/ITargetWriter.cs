using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasswordHashing 
{
    public interface ITargetWriter 
    {
        Task Write(IEnumerable<SourceRecord> values);
    }
}