using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProjectApp.Common.Enum;
using ProjectApp.Data;
using ProjectApp.Interface.Factory;
using ProjectApp.Model.Entities;

namespace ProjectApp.Service.Factory
{
    public class TextFactory : ITextFactory
    {
        private readonly ILogger<TextFactory> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public TextFactory(ILogger<TextFactory> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Text> GetText(TextSourceType textSourceType)
        {
            Text text = null;

            switch (textSourceType)
            {
                case TextSourceType.Database:
                    text = await _unitOfWork.TextRepository().GetById(1);
                    break;
                case TextSourceType.CosmosDb:
                    // text = await _unitOfWork.TextRepository().GetById(1);
                    break;
                default:
                    break;
            }

            return text;
        }
    }
}