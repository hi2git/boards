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
		this.value = value;
		router.setSearch(PARAM, value);
	};

	@action mount = () => {
		const value = router.getSearch(PARAM);
		this.value = (value as number) ?? DEFAULT_VALUE;
	};
}

export default new BoardScale();
