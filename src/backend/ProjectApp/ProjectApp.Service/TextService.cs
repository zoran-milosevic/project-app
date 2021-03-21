using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProjectApp.Data;
using ProjectApp.Model.Entities;
using ProjectApp.Interface.Service;
using ProjectApp.Common.Enum;
using ProjectApp.Interface.Factory;

namespace ProjectApp.Service
{
    public class TextService : ITextService
    {
        private readonly ILogger<TextService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITextFactory _textFactory;

        public TextService(ILogger<TextService> logger, IUnitOfWork unitOfWork, ITextFactory textFactory)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _textFactory = textFactory;
        }

        public async Task<Text> GetTextAsync(TextSourceType sourceType)
        {
            var text = await _textFactory.GetText(sourceType);

            return text;
        }

        public async Task<int> GetTextWordsCountAsync(string text)
        {
            return await GetLenght(text);
        }

        private async Task<int> GetLenght(string text)
        {
            var delimiters = new char[] { ' ', '\r', '\n' };

            return await Task.Run(() =>
            {
                return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
            });
        }
    }
}
