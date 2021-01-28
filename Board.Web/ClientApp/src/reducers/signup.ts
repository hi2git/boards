import { action, computed, makeAutoObservable, observable } from "mobx";

import { IUserLogin } from "../interfaces/components";
import service from "../services/user";
import * as urls from "../constants/urls";

class SignUp {
	constructor() {
		makeAutoObservable(this);
	}

	@observable item: IUserLogin = {};
	@observable isLoading: boolean = false;
	@observable error: string | undefined = undefined;

	@computed get isAllowSend() {
		return !this.isLoading && !this.isPasswordError;
	}

	@computed get isPasswordError() {
		return false; //this.item.newPassword !== this.item.confirmPassword;
	}

	@action reload = () => {
		this.item = {};
		this.clear();
	};

	@action set = (key: string, value?: string) => {
		this.item[key as keyof IUserLogin] = value;
		this.error = undefined;
	};

	@action send = async () => {
		this.isLoading = true;
		let error = undefined;
		try {
			await service.post(this.item);
			window.location.pathname = urls.HOME; // TODO : SPA redirect
		} catch (e) {
			error = e;
		}
		this.clear(error);
	};

	@action private clear = (error?: string) => {
		this.isLoading = false;
		this.error = error;
	};
}

export default new SignUp();
