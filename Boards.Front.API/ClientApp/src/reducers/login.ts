import { action, makeAutoObservable, observable } from "mobx";

import service from "../services/login";
import { IUserLogin } from "../interfaces/components";
import router from "./router";
import boards from "./boards";
import board from "./board";
import posts from "./posts";
import * as urls from "../constants/urls";

class Login {
	constructor() {
		makeAutoObservable(this);
	}

	@observable isLoading: boolean = false;
	@observable error: string | undefined = undefined;

	@action login = async (item: IUserLogin) => {
		this.request();
		try {
			await service.post(item);
			this.receive();
			boards.clear();
			router.push(urls.HOME);
		} catch (e) {
			this.receive(e);
		}
	};

	@action logout = async () => {
		this.request();
		try {
			await service.delete();
			// await boards.clear();
			this.redirect();
		} catch (e) {
			this.receive(e);
		}
	};

	@action redirect = () => {
		this.receive();
		router.push(urls.LOGIN);
	};

	@action private request = () => {
		this.isLoading = true;
		this.error = undefined;
	};

	@action private receive = (error?: any) => {
		this.isLoading = false;
		this.error = error as string;
	};
}

export default new Login();
