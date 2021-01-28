import { IBoardItem } from "../interfaces/components";
import axios from "./api.axios";

import qs from "query-string";

// let items: Array<IBoardItem> = [
// {
// 	id: "001",
// 	orderNumber: 0,
// 	description: "Описание 001",
// },
// {
// 	id: "002",
// 	orderNumber: 1,
// 	description: "Описание 002",
// },
// {
// 	id: "003",
// 	orderNumber: 2,
// 	description: "Описание 003",
// },
// {
// 	id: "004",
// 	orderNumber: 3,
// 	description: "Описание 004",
// },
// {
// 	id: "005",
// 	orderNumber: 4,
// 	description: "Описание 005",
// },
// {
// 	id: "006",
// 	orderNumber: 5,
// 	description: "Описание 006",
// },
// {
// 	id: "007",
// 	orderNumber: 6,
// 	description: "Описание 007",
// },
// {
// 	id: "008",
// 	orderNumber: 7,
// 	description: "Описание 008",
// },
// ];

// const getAllItems: () => Array<IBoardItem> = () => [...items, { id: "add", orderNumber: items.length }];

export default class Service {
	getAll = async () => {
		const url = `/api/boards`;
		const result = await axios.get<Array<IBoardItem>>(url);
		return result.data;
	};

	sort = async (items: Array<IBoardItem>) => {
		const url = `/api/boards`;
		const itms = items.map(n => ({ ...n, id: n.id !== "" ? n.id : null, content: undefined }));
		await axios.put(url, itms);
	};

	post = async (item: IBoardItem) => {
		const url = "/api/board";
		await axios.post(url, { ...item, id: null });
	};

	putContent = async (item: IBoardItem) => {
		const url = "/api/board/content";
		await axios.put(url, item);
	};

	put = async (item: IBoardItem) => {
		const url = "/api/board";
		await axios.put(url, { ...item, content: undefined });
	};

	del = async (id: string) => {
		const paramStr = qs.stringify({ id });
		const url = `/api/board?${paramStr}`;
		await axios.delete(url);
	};
}
