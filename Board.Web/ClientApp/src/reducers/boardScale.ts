import { action, makeAutoObservable, observable } from "mobx";
import router from "./router";

const DEFAULT_VALUE = 1;
const PARAM = "scale";

class BoardScale {
	constructor() {
		makeAutoObservable(this);
	}

	@observable value: number = DEFAULT_VALUE;

	@action setValue = (value: number) => {
		this.value = Math.min(value, 1);
		this.value = Math.max(value, 0.1);
		router.setSearch(PARAM, this.value, DEFAULT_VALUE);
	};

	@action mount = () => {
		const value = router.getSearch(PARAM) as number;
		this.setValue(value ?? DEFAULT_VALUE);
	};
}

export default new BoardScale();
