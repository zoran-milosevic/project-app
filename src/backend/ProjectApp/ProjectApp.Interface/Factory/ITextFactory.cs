using System.Threading.Tasks;
using ProjectApp.Common.Enum;
using ProjectApp.Model.Entities;

namespace ProjectApp.Interface.Factory
{
    public interface ITextFactory
    {
        Task<Text> GetText(TextSourceType textSourceType);
    }
}
