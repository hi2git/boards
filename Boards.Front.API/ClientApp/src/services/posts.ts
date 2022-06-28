import qs from "query-string";

import { IPost } from "../interfaces/components";
import axios from "./api.axios";

class Service {
	getAll = async (boardId?: string) => {
		if (!boardId) {
			console.error("boardId is undefined");
			return [];
		}
		const url = `/api/posts?id=${boardId}`;
		const result = await axios.get<Array<IPost>>(url);
		return result.data;
	};

	sort = async (items: Array<IPost>, boardId?: string) => {
		if (!boardId) return console.error("boardId is undefined");
		const url = `/api/posts`;
		const itms = items.map(n => ({ ...n, id: n.id !== "" ? n.id : null, content: undefined }));
		await axios.put(url, { id: boardId, items: itms });
	};

	post = async (item: IPost, boardId?: string) => {
		if (!boardId) return console.error("boardId is undefined");
		const url = "/api/post";
		await axios.post(url, { id: boardId, item: { ...item, id: null } });
	};

	putContent = async (item: IPost, boardId?: string) => {
		if (!boardId) return console.error("boardId is undefined");
		const url = "/api/post/content";
		await axios.put(url, { id: boardId, item });
	};

	put = async (item: IPost, boardId?: string) => {
		if (!boardId) return console.error("boardId is undefined");
		const url = "/api/post";
		await axios.put(url, { id: boardId, item: { ...item, content: undefined } });
	};

	del = async (id: string, boardId?: string) => {
		if (!boardId) return console.error("boardId is undefined");
		const paramStr = qs.stringify({ id: boardId, item: id });
		const url = `/api/post?${paramStr}`;
		await axios.delete(url);
	};
}

export default new Service();
