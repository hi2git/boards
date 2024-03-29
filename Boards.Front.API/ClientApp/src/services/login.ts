import { IUserLogin } from "../interfaces/components";
import axios from "./api.axios";

export class Service {
	post = async (item: IUserLogin) => {
		await axios.post(this.url, item);
	};

	delete = async () => {
		await axios.delete(this.url);
	};

	private get url() {
		return "/api/account";
	}
}

export default new Service();
