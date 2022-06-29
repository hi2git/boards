import qs from "query-string";

import { IIdName } from "../interfaces/components";
import axios from "./api.axios";

class Service {
	getAll = async () => {
		const url = "/api/boards";
		const result = await axios.get<Array<IIdName>>(url);
		return result.data;
	};

	post = async (value: string) => {
		const url = "/api/board";
		const result = await axios.post<string>(url, { value });
		return result.data;
	};

	put = async (item: IIdName) => {
		const url = "/api/board";
		await axios.put(url, { ...item, content: undefined });
	};

	del = async (id: string) => {
		const paramStr = qs.stringify({ id });
		const url = `/api/board?${paramStr}`;
		await axios.delete(url);
	};
}

export default new Service();