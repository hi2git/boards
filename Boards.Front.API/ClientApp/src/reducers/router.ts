import { action, computed, makeAutoObservable } from "mobx";
import { RouterStore } from "mobx-react-router";
import qs from "query-string";

import * as urls from "../constants/urls";
// import sidebar from "./boardSidebar";

class CustomRouter {
	private static _router = new RouterStore();

	constructor() {
		makeAutoObservable(this);
	}

	@computed public get router() {
		return CustomRouter._router;
	}

	@computed private get search() {
		return qs.parse(this.router.location?.search, { parseBooleans: true, parseNumbers: true });
	}

	@computed get startPath() {
		const path = "/" + this.router.location?.pathname.split("/")[1]?.toLowerCase();
		switch (path) {
			case urls.SETTINGS:
				return urls.SETTINGS;
			case urls.CONTACTS:
				return urls.CONTACTS;
			default:
				return urls.HOME;
		}
	}

	@action getSearch = (key: string) => this.search[key];

	@action setSearch = (key: string, value?: string | number | boolean, def?: typeof value) => {
		const search = this.search;
		search[key] = def === value ? undefined : (value as any);

		const paramStr = qs.stringify(search);
		this.push(`?${paramStr}`);
	};

	get boardName() {
		return this.router.location?.pathname.split("/")[1]?.toLowerCase();
	}

	@action setBoardName(value: string) {
		this.setSearch("index");
		const search = this.router.location?.search;

		const path = `${value}${search}`;
		this.push(path);
	}

	@action push = (value: string) => this.router.push(value);
}

export default new CustomRouter();
