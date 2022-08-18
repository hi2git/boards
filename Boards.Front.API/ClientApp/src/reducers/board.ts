import { action, makeAutoObservable, observable } from "mobx";

import router from "./router";
import boards from "./boards";
// import posts from "./posts";
import { IIdName } from "../interfaces/components";

const DEFAULT_VALUE = undefined;
const PARAM = "board";

class Store {
	constructor() {
		makeAutoObservable(this);
	}

	@observable value: IIdName | undefined = DEFAULT_VALUE;

	@action setValue = async (value?: IIdName) => {
		this.value = value;
		router.setSearch(PARAM, value?.id, DEFAULT_VALUE);
		// if (!!value) await posts.fetchAll();
	};

	@action clear = () => this.setValue();

	@action mount = async (defValue?: IIdName) => {
		const value = router.getSearch(PARAM) as string;
		await this.setValue(boards.get(value) ?? defValue ?? DEFAULT_VALUE);
	};
}

export default new Store();
