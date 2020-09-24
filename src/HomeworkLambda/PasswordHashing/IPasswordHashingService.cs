using System.Threading.Tasks;

namespace PasswordHashing 
{
    public interface IPasswordHashingService
    {
        Task Execute(string sourcePath);
    }
}