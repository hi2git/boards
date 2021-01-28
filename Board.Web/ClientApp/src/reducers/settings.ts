import { action, computed, observable, makeAutoObservable } from "mobx";
import { IUserSettings } from "../interfaces/components";
import service from "../services/settings";

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
		this.clear();
	};

	@action set = (key: string, value?: string) => {
		this.item[key as keyof IItem] = value;
		this.error = undefined;
	};

	@action send = async () => {
		this.isLoading = true;
		let error = undefined;
		try {
			await service.put(this.dto);
		} catch (e) {
			error = e;
		}
		this.clear(error);
	};

	@action clear = (error?: string) => {
		// this.item = {};
		this.isLoading = false;
		this.error = error;
	};
}

export default new Settings();
