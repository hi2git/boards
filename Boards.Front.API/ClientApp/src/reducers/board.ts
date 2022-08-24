import { action, makeAutoObservable, observable, computed } from "mobx";

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
	// @computed get value() {
	// 	console.log("computed selected board", router.boardName);
	// 	return boards.getByName(router.boardName);
	// }

	@action setValue = async (value?: IIdName) => {
		this.value = value;
		router.setBoardName(value?.name ?? "");
	};

	@action clear = () => (this.value = DEFAULT_VALUE);

	@action mount = async (defValue?: IIdName) => {
		const value = router.boardName;
		await this.setValue(boards.getByName(value) ?? defValue ?? DEFAULT_VALUE);
	};
}

export default new Store();
