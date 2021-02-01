import { action, computed, makeAutoObservable } from "mobx";
import { RouterStore } from "mobx-react-router";
import qs from "query-string";

class CustomRouter {
	private _router = new RouterStore();

	constructor() {
		makeAutoObservable(this);
	}

	public get router() {
		return this._router;
	}

	@computed private get search() {
		return qs.parse(this.router.location.search, { parseBooleans: true, parseNumbers: true });
	}

	@action getSearch = (key: string) => this.search[key];

	@action setSearch = (key: string, value?: string | number | boolean, def?: typeof value) => {
		const search = this.search;
		search[key] = def === value ? undefined : (value as any);

		const paramStr = qs.stringify(search);
		this.router.push(`?${paramStr}`);
	};

	@action push = (value: string) => this.router.push(value);
}

export default new CustomRouter();
