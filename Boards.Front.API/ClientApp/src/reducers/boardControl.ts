import { action, makeAutoObservable, observable } from "mobx";
import router from "./router";

const DEFAULT_VALUE = false;
const PARAM = "control";

class BoardControl {
	constructor() {
		makeAutoObservable(this);
	}

	@observable value: boolean = DEFAULT_VALUE;

	@action private setValue = (value: boolean) => {
		this.value = value;
		router.setSearch(PARAM, this.value, DEFAULT_VALUE);
	};

	@action toggle = () => this.setValue(!this.value);

	@action mount = () => {
		const value = router.getSearch(PARAM) as boolean;
		this.setValue(value ?? DEFAULT_VALUE);
	};
}

export default new BoardControl();
