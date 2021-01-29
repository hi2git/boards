import { action, makeAutoObservable, observable } from "mobx";

class BoardScale {
	constructor() {
		makeAutoObservable(this);
	}

	@observable scale: number = 1;

	@action setScale = (value: number) => (this.scale = value);
}

export default new BoardScale();
