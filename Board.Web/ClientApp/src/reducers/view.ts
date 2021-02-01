import { action, makeAutoObservable, observable } from "mobx";
import { Property } from "csstype";

import router from "./router";

const DEFAULT_VALUE = "cover";
const PARAM = "view";

class View {
	constructor() {
		makeAutoObservable(this);
	}

	@observable value: Property.ObjectFit = DEFAULT_VALUE;

	@action setValue = async (value: Property.ObjectFit) => {
		this.value = value;
		router.setSearch(PARAM, value, DEFAULT_VALUE);
	};

	@action mount = () => {
		const value = router.getSearch(PARAM) as Property.ObjectFit;
		this.setValue(value ?? DEFAULT_VALUE);
	};
}

export default new View();
