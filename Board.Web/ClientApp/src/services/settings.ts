import axios from "./api.axios";
import { IUserSettings } from "../interfaces/components";

export class Service {
	put = async (item: IUserSettings) => {
		const url = "/api/settings";
		const result = await axios.put(url, item);
		return result.data;
	};
}

export default new Service();
