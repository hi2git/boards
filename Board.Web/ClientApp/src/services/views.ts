import { IIdName } from "../interfaces/components";

const items: Array<IIdName> = [
	{ id: "cover", name: "Центрировать" },
	{ id: "fill", name: "По ширине" },
	{ id: "scale-down", name: "По высоте" },
	// { id: "contain", name: "contain" },
	// { id: "none", name: "none" },
	// { id: "unset", name: "unset" },
	// { id: "revert", name: "revert" },
];

export default class Service {
	getAll = () => Promise.resolve(items);
}
