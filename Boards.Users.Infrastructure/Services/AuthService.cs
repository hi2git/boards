﻿using System;

using Boards.Commons.Domain.DTOs.Jwt;
using Boards.Commons.Domain.DTOs.Users;
using Boards.Users.Application;
using Boards.Users.Domain.Models;
using Boards.Users.Domain.Repos;

namespace Boards.Users.Infrastructure.Services {
	internal class AuthService : IAuthService {
		private readonly IUserRepo _userRepo;
		private readonly IPasswordService _userService;
		private readonly ITokenService _tokenService;

		public AuthService(IUserRepo userRepo, IPasswordService userService, ITokenService tokenService) {
			_userRepo = userRepo;
			_userService = userService;
			_tokenService = tokenService;
		}

		public async Task<JwtTokenDTO> GetToken(LoginDTO dto, AuthSettings auth, CancellationToken token) {
			var entity = await _userRepo.Get(dto.Login, token) ?? throw new ArgumentException("Некорректное имя пользователя или пароль");

			if (!_userService.IsEqual(dto.Password, entity.Password))
				throw new ArgumentException($"Некорректное имя пользователя или пароль");

			return await this.GetToken(entity, auth);
		}

		public Task<JwtTokenDTO> GetToken(User user, AuthSettings auth) {
			var dto = Map(user);
			return _tokenService.Generate(dto, auth);
		}

		private static UserLoginDTO Map(User u) => new() {
			Id = u.Id,
			Name = u.Name,
			Role = u.RoleId.ToString("G") 
		};

	}
}
