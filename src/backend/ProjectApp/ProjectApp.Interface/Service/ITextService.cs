using System.Threading.Tasks;
using ProjectApp.Domain.Entities;

namespace ProjectApp.Interface.Service
{
    public interface ITextService
    {
        Task<int> GetTextWordsCountAsync(string text);
        Task<Text> GetTextAsync();
    }
}