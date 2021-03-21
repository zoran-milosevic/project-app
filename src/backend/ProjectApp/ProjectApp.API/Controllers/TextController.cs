using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ProjectApp.Interface.Service;
using ProjectApp.Api.Validations;
using ProjectApp.BindingModel;
using ProjectApp.Model.Entities;
using ProjectApp.Common.Enum;
using ProjectApp.DTO.Text;

namespace ProjectApp.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json"), Produces("application/json")]
    public class TextController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITextService _textService;

        public TextController(IMapper mapper, ITextService textService)
        {
            _mapper = mapper;
            _textService = textService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetText()
        {
            var txt = await _textService.GetTextAsync(TextSourceType.Database);

            var result = _mapper.Map<Text, TextFromDbDTO>(txt);

            return Ok(result);
        }

        [HttpPost]
        [Route("Count")]
        public async Task<IActionResult> Count([FromBody] TextBindingModel model)
        {
            var validator = new TextValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors); // needs refining
            }

            var count = await _textService.GetTextWordsCountAsync(model.Text);

            var dto = new TextDTO()
            {
                TextLength = count,
                StatusCode = count != 0 ? 1 : 0,
                StatusMessage = count != 0 ? string.Empty : "Error occured during an count operation."
            };

            return Ok(dto);
        }
    }
}
