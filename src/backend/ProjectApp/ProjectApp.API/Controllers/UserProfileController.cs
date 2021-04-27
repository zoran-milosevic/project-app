using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ProjectApp.Interface.Service;
using ProjectApp.Model.Entities.User;
using ProjectApp.Api.Validations;
using ProjectApp.Model.Binding;
using ProjectApp.Model.DTO;

namespace ProjectApp.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json"), Produces("application/json")]
    public class UserProfileController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IMapper mapper, IUserProfileService userProfileService)
        {
            _mapper = mapper;
            _userProfileService = userProfileService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllProfiles()
        {
            var profiles = await _userProfileService.GetAllAsync();

            var result = _mapper.Map<IEnumerable<UserProfile>, IEnumerable<UserProfileDTO>>(profiles);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var profile = await _userProfileService.GetByIdAsync(id);

            var result = _mapper.Map<UserProfile, UserProfileDTO>(profile);

            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateUserProfile([FromBody] UserProfileBindingModel model)
        {
            var validator = new UserProfileValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors); // needs refining
            }

            var userProfileToCreate = _mapper.Map<UserProfileBindingModel, UserProfile>(model);

            var newProfile = await _userProfileService.CreateAsync(userProfileToCreate);
            var userProfile = await _userProfileService.GetByIdAsync(newProfile.UserProfileId);

            var result = _mapper.Map<UserProfile, UserProfileDTO>(userProfile);

            return Ok(result);
        }
    }
}