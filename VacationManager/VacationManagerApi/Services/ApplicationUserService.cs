using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VacationManagerApi.Helpers;
using VacationManagerApi.Models.Entities;
using VacationManagerApi.Models.Requests;
using VacationManagerApi.Models.Responses;
using VacationManagerApi.Repositories;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

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
        private readonly IApplicationUserRepository repository;
        private readonly SignInManager<ApplicationUser> signInManager;

        public ApplicationUserService(
            IMapper mapper,
            IConfiguration configuration,
            IApplicationUserRepository repository,
            SignInManager<ApplicationUser> signInManager
        )
        {
            this.mapper = mapper;
            this.configuration = configuration;
            this.repository = repository;
            this.signInManager = signInManager;
        }

        public Task<IBaseResponse> Register(RegisterUserRequest request)
        {
            return ResponseHandler.HandleErrors(
                async () =>
                {
                    var result = await repository
                        .Add(mapper.Map<ApplicationUser>(request), request.Password)
                        .ConfigureAwait(false);

                    return result.Succeeded
                        ? ResponseHandler.Success(request)
                        : ResponseHandler.Validations(result.Errors.Select(x => x.Description));
                }
            );
        }

        public Task<IBaseResponse> SingIn(SingInRequest request)
        {
            return ResponseHandler.HandleErrors(
                async () =>
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
                        return ResponseHandler.Unauthorized("Invalid credentials");
                    }

                    var token = await GenerateToken(request.UserName).ConfigureAwait(false);

                    return ResponseHandler.Success(token);
                }
            );
        }

        private async Task<string> GenerateToken(string userName)
        {
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(30),
                claims: await GetClaims(userName).ConfigureAwait(false),
                signingCredentials: new SigningCredentials(
                    key: new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Secret"])),
                    algorithm: SecurityAlgorithms.HmacSha256Signature
                )
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<List<Claim>> GetClaims(string userName)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, userName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var roles = await repository
                .GetRoles(userName)
                .ConfigureAwait(false);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }
    }
}
