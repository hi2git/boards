using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Board.Domain.Repos;
using Board.Domain.Services;

using Microsoft.AspNetCore.Mvc;

namespace Board.Web.Controllers {
	public class SettingsController : AbstractApiController {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepo _userRepo;
		private readonly IUserManager _userMgr;
		private readonly IPasswordService _pwdService;

		public SettingsController(IUnitOfWork unitOfWork, IUserRepo userRepo, IUserManager userMgr, IPasswordService pwdService) {
			_unitOfWork = unitOfWork;
			_userRepo = userRepo;
			_userMgr = userMgr;
			_pwdService = pwdService;
		}

		//[HttpGet]
		//public async Task<bool> Get([FromQuery] )

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
