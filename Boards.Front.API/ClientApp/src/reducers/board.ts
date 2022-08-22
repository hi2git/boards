import { action, makeAutoObservable, observable } from "mobx";

import router from "./router";
import boards from "./boards";
import { IIdName } from "../interfaces/components";

const DEFAULT_VALUE = undefined;
// const PARAM = "board";

class Store {
	constructor() {
		makeAutoObservable(this);
	}

	@observable value: IIdName | undefined = DEFAULT_VALUE;

	@action setValue = async (value?: IIdName) => {
		this.value = value;
		// router.setBoard(PARAM, value?.id, DEFAULT_VALUE);
		router.boardName = value?.name ?? "";
	};

	@action clear = () => this.setValue();

	@action mount = async (defValue?: IIdName) => {
		const value = router.boardName;
		await this.setValue(boards.getByName(value) ?? defValue ?? DEFAULT_VALUE);
	};
}

export default new Store();
