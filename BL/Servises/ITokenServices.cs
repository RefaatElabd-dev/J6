using J6.DAL.Entities;
using System.Threading.Tasks;

namespace J6.BL.Servises
{
    public interface ITokenServices
    {
        Task<string> CreateToken(AppUser appUser);
    }
}
