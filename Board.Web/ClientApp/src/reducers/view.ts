import { action, makeAutoObservable, observable } from "mobx";
import { Property } from "csstype";

class View {
	constructor() {
		makeAutoObservable(this);
	}

	@observable selected: Property.ObjectFit = "cover";

	@action setSelected = async (value: Property.ObjectFit) => (this.selected = value);
}

export default new View();
