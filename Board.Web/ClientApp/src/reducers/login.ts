import { action, makeAutoObservable, observable } from "mobx";

import service from "../services/login";
import { IUserLogin } from "../interfaces/components";
import router from "./router";
import boards from "./boards";
import board from "./board";
import boardItems from "./boardItems";
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

			boardItems.clear();
			boards.clear();
			board.clear();

			router.push(urls.HOME);
		} catch (e) {
			this.receive(e);
		}
	};

	@action logout = async () => {
		this.request();
		try {
			await service.delete();
			this.receive();
			router.push(urls.LOGIN);
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

export default new Login();
