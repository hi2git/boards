using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Board.Domain.DTO;
using Board.Domain.Enums;
using Board.Domain.Models;
using Board.Domain.Repos;
using Board.Domain.Services;
using Board.Infrastructure.Jwt.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Board.Web.Controllers {
	public class UserController : AbstractApiController {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepo _userRepo;
		private readonly IRepo<Role> _roleRepo;
		private readonly IUserManager _userMgr;
		private readonly IPasswordService _pwdService;
		private readonly IAuthService _authService;
		private readonly IBoardRepo _boardRepo;

		public UserController(IUnitOfWork unitOfWork, IUserRepo userRepo, IRepo<Role> roleRepo, IUserManager userMgr, IPasswordService pwdService, IAuthService authService, IBoardRepo boardRepo) {
			_unitOfWork = unitOfWork;
			_userRepo = userRepo;
			_roleRepo = roleRepo;
			_userMgr = userMgr;
			_pwdService = pwdService;
			_authService = authService;
			_boardRepo = boardRepo;
		}

		[HttpPost, AllowAnonymous]
		public async Task Create([FromBody] LoginDTO dto) { // Check Captcha
			var passwordHash = _pwdService.Hash(dto.Password);
			var role = await _roleRepo.Query.FirstOrDefaultAsync(n => n.Id == RoleEnum.User);

			var user = new User(Guid.NewGuid(), dto.Login, passwordHash, role);
			await _userRepo.Create(user);

			var board = new Domain.Models.Board(Guid.NewGuid(), "MyFirstDesk", user);
			await _boardRepo.Create(board);

			await _unitOfWork.Commit();

			await _authService.Login(user);
		}


		[HttpPut]
		public async Task Update([FromBody] UserSettingsDTO dto) {
			if (!dto.IsPasswordChanged)
				return;

			var user = await _userRepo.Get(_userMgr.CurrentUserId);
			var isOldRight = _pwdService.IsEqual(dto.OldPassword, user.Password);
			if (!isOldRight)
				throw new ArgumentException("Старый пароль введен неверно");

			user.Password = _pwdService.Hash(dto.NewPassword);
			await _userRepo.Update(user);
			await _unitOfWork.Commit();
		}

	}

	public class UserSettingsDTO {

		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
		public bool IsPasswordChanged { get; set; }

	}
}
