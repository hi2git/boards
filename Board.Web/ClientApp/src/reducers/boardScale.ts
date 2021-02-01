import { action, makeAutoObservable, observable } from "mobx";
import router from "./router";

const MIN = 1;
const MAX = 100;

const DEFAULT_VALUE = MAX;
const PARAM = "scale";

class BoardScale {
	constructor() {
		makeAutoObservable(this);
	}

	@observable value: number = DEFAULT_VALUE;

	@action setValue = (value: number) => {
		this.value = Math.min(value, MAX);
		this.value = Math.max(value, MIN);
		router.setSearch(PARAM, this.value, DEFAULT_VALUE);
	};

	@action mount = () => {
		const value = router.getSearch(PARAM) as number;
		this.setValue(value ?? DEFAULT_VALUE);
	};
}

export default new BoardScale();
