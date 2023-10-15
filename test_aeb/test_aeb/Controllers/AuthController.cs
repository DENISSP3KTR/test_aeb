using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using TestAEB.Models;

namespace TestAEB.Controllers
{
    /// <summary>
    /// Controller for user authentication and authorization.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly User user = new User();
        private readonly IConfiguration _conf;
        /// <summary>
        /// auth controller constructor
        /// </summary>
        /// <param name="conf">Application Configuration</param>
        public AuthController(IConfiguration conf)
        {
            _conf = conf;
        }
        /// <summary>
        /// Registers a new user and returns information about him.
        /// </summary>
        /// <param name="request">User data for registration</param>
        /// <returns>Information about the registered user</returns>
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO request)
        {
            PassHasher(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return Ok(user);
        }
        /// <summary>
        /// User authentication and generation of a JWT token for authorization
        /// </summary>
        /// <param name="request">User data for authentication</param>
        /// <returns>JWT token in case of successful authentication</returns>
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDTO request)
        {
            if (user.Username != request.Username)
            {
                return BadRequest("Пользователь не найден");
            }
            if (!PassHashBool(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Пароль не совпадает");
            }
            string token = CreateToken(user);

            return Ok(token);
        }
        /// <summary>
        /// Creates a JWT token based on user information
        /// </summary>
        /// <param name="user">The user for whom the token is being created</param>
        /// <returns>Generated JWT token</returns>
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_conf.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        /// <summary>
        /// Hashes the users password and creates Salt
        /// </summary>
        /// <param name="password">user password</param>
        /// <param name="passwordHash">password hash</param>
        /// <param name="passwordSalt">salt password</param>
        private static void PassHasher(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        /// <summary>
        /// Checks whether the password hash matches the hash in the database
        /// </summary>
        /// <param name="password">password to check</param>
        /// <param name="passHash">password hash from the database</param>
        /// <param name="passSalt">password-related salt</param>
        /// <returns>True if the password matches the hash in the database, otherwise false</returns>
        private static bool PassHashBool(string password, byte[] passHash, byte[] passSalt) 
        {
            using(var hmac = new HMACSHA256(passSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passHash);
            }
        }
    }
}
