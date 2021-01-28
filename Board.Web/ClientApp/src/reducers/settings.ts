import { action, computed, observable, makeAutoObservable } from "mobx";
import { IUserSettings } from "../interfaces/components";
import service from "../services/settings";

class Settings {
	constructor() {
		makeAutoObservable(this);
	}

	@observable oldPassword: string | undefined = undefined;
	@observable newPassword: string | undefined = undefined;
	@observable confirmPassword: string | undefined = undefined;

	@computed get isAllowSend() {
		return !!this.oldPassword && this.isPasswordChanged && !this.isPasswordError;
	}

	@computed get isPasswordChanged() {
		return !!this.newPassword;
	}

	@computed get isPasswordError() {
		return this.newPassword !== this.confirmPassword;
	}

	@computed get item(): IUserSettings {
		const { oldPassword, newPassword, isPasswordChanged } = this;
		return { oldPassword, newPassword, isPasswordChanged };
	}

	@action setOldPassword = (value?: string) => (this.oldPassword = value);
	@action setNewPassword = (value?: string) => (this.newPassword = value);
	@action setConfirmPassword = (value?: string) => (this.confirmPassword = value);

	@action send = async () => {
		await service.put(this.item);
		this.clear();
	};

	@action clear = () => {
		this.setOldPassword();
		this.setNewPassword();
		this.setConfirmPassword();
	};
}

export default new Settings();
