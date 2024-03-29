import axios from "./api.axios";
import { IUserLogin, IUserSettings } from "../interfaces/components";

export class Service {
	post = async (item: IUserLogin) => {
		const url = "/api/user";
		const result = await axios.post(url, item);
		return result.data;
	};

	put = async (item: IUserSettings) => {
		const url = "/api/user";
		const result = await axios.put(url, item);
		return result.data;
	};
	del = async () => {
		const url = "/api/user";
		await axios.delete(url);
	};
}

export default new Service();
