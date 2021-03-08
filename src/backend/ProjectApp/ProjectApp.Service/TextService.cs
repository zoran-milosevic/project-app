using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProjectApp.Data;
using ProjectApp.Domain.Entities;
using ProjectApp.Interface.Service;

namespace ProjectApp.Service
{
    public class TextService : ITextService
    {
        private readonly ILogger<TextService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public TextService(ILogger<TextService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Text> GetTextAsync()
        {
            return await _unitOfWork.TextRepository().GetById(1);
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
