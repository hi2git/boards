using System;
using System.Threading.Tasks;

using Board.Application.Services;
using Board.Domain.DTO.Users;
using Board.Domain.Models;
using Board.Domain.Repos;
using Board.Domain.Services;
using Board.Infrastructure.Jwt.Interfaces;

namespace Board.Infrastructure.Jwt.Implementation {
	internal class AuthService : IAuthService {
		private readonly ICookieService _cookieService;
		private readonly IUserRepo _userRepo;
		private readonly IPasswordService _userService;
		private readonly ITokenService _tokenService;

		public AuthService(ICookieService cookieService, IUserRepo userRepo, IPasswordService userService, ITokenService tokenService) {
			_cookieService = cookieService;
			_userRepo = userRepo;
			_userService = userService;
			_tokenService = tokenService;
		}

		public async Task Login(LoginDTO dto) {
			var entity = await _userRepo.Get(dto.Login) ?? throw new ArgumentException("Некорректное имя пользователя или пароль");

			if (!_userService.IsEqual(dto.Password, entity.Password))
				throw new ArgumentException($"Некорректное имя пользователя или пароль");

			await this.Login(entity);
		}

		public async Task Login(User user) {
			var loginDto = this.Map(user);
			var token = await _tokenService.Generate(loginDto);
			_cookieService.Add(token);
		}

		public Task Logout() => Task.Run(_cookieService.Remove);

		private UserLoginDTO Map(User u) => new UserLoginDTO {
			Id = u.Id,
			Name = u.Name,
			PasswordHash = u.Password,
			Role = new RoleDTO { Id = u.RoleId, Name = u.Role.Name }
		};

	}
}
