using System.Threading.Tasks;
using ProjectApp.Common.Enum;
using ProjectApp.Model.Entities;

namespace ProjectApp.Interface.Service
{
    public interface ITextService
    {
        Task<int> GetTextWordsCountAsync(string text);
        Task<Text> GetTextAsync(TextSourceType sourceType);
    }
}