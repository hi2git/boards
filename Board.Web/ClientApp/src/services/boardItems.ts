import qs from "query-string";

import { IBoardItem } from "../interfaces/components";
import axios from "./api.axios";

class Service {
	getAll = async (boardId: string) => {
		const url = `/api/boardItems?id=${boardId}`;
		const result = await axios.get<Array<IBoardItem>>(url);
		return result.data;
	};

	sort = async (boardId: string, items: Array<IBoardItem>) => {
		const url = `/api/boardItems`;
		const itms = items.map(n => ({ ...n, id: n.id !== "" ? n.id : null, content: undefined }));
		await axios.put(url, { id: boardId, items: itms });
	};

	post = async (boardId: string, item: IBoardItem) => {
		const url = "/api/boardItem";
		await axios.post(url, { id: boardId, item: { ...item, id: null } });
	};

	putContent = async (item: IBoardItem) => {
		const url = "/api/boardItem/content";
		await axios.put(url, item);
	};

	put = async (item: IBoardItem) => {
		const url = "/api/boardItem";
		await axios.put(url, { ...item, content: undefined });
	};

	del = async (id: string) => {
		const paramStr = qs.stringify({ id });
		const url = `/api/boardItem?${paramStr}`;
		await axios.delete(url);
	};
}

export default new Service();
