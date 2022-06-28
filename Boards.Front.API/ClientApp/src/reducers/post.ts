import { action, makeAutoObservable, observable } from "mobx";

import router from "./router";

const DEFAULT_VALUE = undefined;
const PARAM = "item";

class Post {
	constructor() {
		makeAutoObservable(this);
	}

	@observable value: string | undefined = DEFAULT_VALUE;

	@action setValue = async (value?: string) => {
		this.value = value;
		router.setSearch(PARAM, value, DEFAULT_VALUE);
	};

	@action mount = () => {
		const value = router.getSearch(PARAM) as string;
		this.setValue(value ?? DEFAULT_VALUE);
	};
}

export default new Post();
