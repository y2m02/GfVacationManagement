using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HelpersLibrary.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VacationManagerApi.Models.Entities;
using VacationManagerApi.Models.Requests;
using VacationManagerApi.Models.Responses;

namespace VacationManagerApi.Services
{
    public interface IApplicationUserService
    {
        Task<IBaseResponse> Register(RegisterUserRequest request);
        Task<IBaseResponse> SingIn(SingInRequest request);
    }

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public ApplicationUserService(
            IMapper mapper,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
        )
        {
            this.mapper = mapper;
            this.configuration = configuration;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IBaseResponse> Register(RegisterUserRequest request)
        {
            var user = mapper.Map<ApplicationUser>(request);

            var result = await userManager
                .CreateAsync(user, request.Password)
                .ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return new Failure(result.Errors.Select(x => x.Description).Join("\n"));
            }

            await userManager.AddToRoleAsync(user, "CanRead").ConfigureAwait(false);

            return new Success(user.Id);
        }

        public async Task<IBaseResponse> SingIn(SingInRequest request)
        {
            var result = await signInManager
                .PasswordSignInAsync(
                    request.UserName,
                    request.Password,
                    isPersistent: false,
                    lockoutOnFailure: false
                )
                .ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return new Failure("Unauthorized");
            }
            
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, request.UserName),
                new(ClaimTypes.Role, "Admin"),
                new(ClaimTypes.Role, "CanRead"),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(30),
                claims: claims,
                signingCredentials: new SigningCredentials(
                    key: new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Secret"])),
                    algorithm: SecurityAlgorithms.HmacSha256Signature
                )
            );

            return new Success(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
