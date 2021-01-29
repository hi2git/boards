import { action, autorun, makeAutoObservable, observable } from "mobx";

class BoardControl {
	constructor() {
		makeAutoObservable(this);
		autorun(() => console.log(this.isVisible));
	}

	@observable isVisible: boolean = false;

	@action setVisible = (value: boolean) => (this.isVisible = value);

	@action toggleVisible = () => (this.isVisible = !this.isVisible);
}

export default new BoardControl();
