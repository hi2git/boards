import { action, makeAutoObservable, observable } from "mobx";

import router from "./router";
import boardItems from "./boardItems";

const DEFAULT_VALUE = undefined;
const PARAM = "board";

class Store {
	constructor() {
		makeAutoObservable(this);
	}

	@observable value: string | undefined = DEFAULT_VALUE;

	@action setValue = async (value?: string) => {
		console.log("set board", value);
		this.value = value;
		router.setSearch(PARAM, value, DEFAULT_VALUE);
		if (!!value) await boardItems.fetchAll();
	};

	@action clear = () => this.setValue();

	@action mount = async (defValue?: string) => {
		const value = router.getSearch(PARAM) as string;
		await this.setValue(value ?? defValue ?? DEFAULT_VALUE);
	};
}

export default new Store();
