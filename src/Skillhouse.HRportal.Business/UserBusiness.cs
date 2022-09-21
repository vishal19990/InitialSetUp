using Microsoft.IdentityModel.Tokens;
using Skillhouse.HRportal.Common;
using Skillhouse.HRportal.Common.Helpers;
using Skillhouse.HRportal.Model;
using Skillhouse.HRportal.Repository.Contracts;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Skillhouse.HRportal.Business
{
    public class UserBusiness
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;
        public UserBusiness(IUserRepository userRepository,AppSettings appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings;
        }

        public async Task<AuthenticationModel> GetUserDetails(LoginModel loginModel)
        {

            var user = (await _userRepository.GetByAsync(x => x.Email == loginModel.UserName)).FirstOrDefault();
            if (user == null){
                return new AuthenticationModel() { Error = "UserName  is incorrect. Please try again!" };
            }
            if (user != null && user.Password != Cryptography.ComputeSHA256Hash(loginModel.Password, user.CreatedOn.ToString("dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)))
            {
                await this._userRepository.SaveAsync();
                return new AuthenticationModel() { Error = "Password is incorrect. Please try again!" };
            }
            
            var authModel = new AuthenticationModel()
            {
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
            return authModel;
        }

        public async Task PopulateJwtTokenAsync(AuthenticationModel authModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.NameIdentifier, authModel.UserId.ToString()),
                        new Claim(ClaimTypes.Email, authModel.Email.ToString()),
                        new Claim(ClaimTypes.Name, authModel.Name.ToString())
                }),
                Expires = authModel.TokenExpiryDate = DateTime.UtcNow.AddMinutes(_appSettings.TokenSettings.SessionExpiryInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            authModel.Token = tokenHandler.WriteToken(token);
        }

    }
}