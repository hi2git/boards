import { action, makeAutoObservable, observable } from "mobx";

class BoardScale {
	constructor() {
		makeAutoObservable(this);
	}

	@observable value: number = 1;

	@action setScale = (value: number) => (this.value = value);
}

export default new BoardScale();
