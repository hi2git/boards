import { action, computed, observable, makeAutoObservable } from "mobx";
import { IUserSettings } from "../interfaces/components";
import service from "../services/user";

import login from "./login";

interface IItem {
	oldPassword?: string;
	newPassword?: string;
	confirmPassword?: string;
}

class Settings {
	constructor() {
		makeAutoObservable(this);
	}

	@observable item: IItem = {};
	@observable isLoading: boolean = false;
	@observable error: string | undefined = undefined;

	@computed get isAllowSend() {
		return !this.isLoading && !!this.item.oldPassword && this.isPasswordChanged && !this.isPasswordError;
	}

	@computed get isPasswordChanged() {
		return !!this.item.newPassword;
	}

	@computed get isPasswordError() {
		return this.item.newPassword !== this.item.confirmPassword;
	}

	@computed private get dto(): IUserSettings {
		const { oldPassword, newPassword } = this.item;
		return { oldPassword, newPassword, isPasswordChanged: this.isPasswordChanged };
	}

	@action reload = () => {
		this.item = {};
		this.receive();
	};

	@action set = (key: string, value?: string) => {
		this.item[key as keyof IItem] = value;
		this.error = undefined;
	};

	@action send = async () => {
		this.request();
		try {
			await service.put(this.dto);
			this.receive();
		} catch (e) {
			this.receive(e);
		}
	};

	@action del = async () => {
		this.request();
		try {
			await service.del();
			this.receive();
			await login.redirect();
		} catch (e) {
			this.receive(e);
		}
	};

	@action private request = () => {
		this.isLoading = true;
		this.error = undefined;
	};

	@action private receive = (error?: string) => {
		this.isLoading = false;
		this.error = error;
	};
}

export default new Settings();
