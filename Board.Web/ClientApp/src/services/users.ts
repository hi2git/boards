import { IIdName } from "../interfaces/components";
import axios from "./api.axios";

export default class Service {
	getAll = async () => {
		const url = "/api/users";
		const result = await axios.get<Array<IIdName>>(url);
		return result.data;
	};
}
