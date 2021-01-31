import { action, makeAutoObservable, observable } from "mobx";
import router from "./router";

const DEFAULT_VALUE = false;
const PARAM = "control";

class BoardControl {
	constructor() {
		makeAutoObservable(this);
	}

	@observable value: boolean = DEFAULT_VALUE;

	@action toggle = () => {
		this.value = !this.value;
		router.setSearch(PARAM, this.value);
	};

	@action mount = () => {
		const value = router.getSearch(PARAM) as boolean;
		this.value = value ?? DEFAULT_VALUE;
	};
}

export default new BoardControl();
