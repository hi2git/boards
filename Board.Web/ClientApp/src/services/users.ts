import { IIdName } from "../interfaces/components";
import axios from "./api.axios";

// const items: Array<IIdName> = [
// 	{
// 		id: "001",
// 		name: "маусихож",
// 	},
// 	{
// 		id: "002",
// 		name: "арболь",
// 	},
// 	{
// 		id: "003",
// 		name: "mouse12x3oz",
// 	},
// ];

export default class Service {
	getAll = async () => {
		const url = "/api/users";
		const result = await axios.get<Array<IIdName>>(url);
		return result.data;
	};
}
