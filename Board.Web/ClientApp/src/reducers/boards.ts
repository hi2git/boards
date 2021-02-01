import { action, makeAutoObservable, observable } from "mobx";

import { IIdName } from "../interfaces/components";
import service from "../services/boards";

class Store {
	constructor() {
		makeAutoObservable(this);
	}

	@observable items: Array<IIdName> = [];
	@observable isLoading: boolean = false;
	@observable error: string | undefined = undefined;

	@action fetchAll = async () => {
		this.request();
		try {
			this.receive(await service.getAll());
		} catch (e) {
			this.receive([], e);
		}
	};

	@action post = async (item: IIdName) => {
		this.request();
		try {
			await service.post(item);
			await this.fetchAll();
		} catch (e) {
			this.receive(undefined, e);
		}
	};
	@action put = async (item: IIdName) => {
		this.request();
		try {
			await service.put(item);
			await this.fetchAll();
		} catch (e) {
			this.receive(undefined, e);
		}
	};
	@action del = async (id: string) => {
		this.request();
		try {
			await service.del(id);
			await this.fetchAll();
		} catch (e) {
			this.receive(undefined, e);
		}
	};

	@action private request = () => {
		this.isLoading = true;
		this.items = [];
		this.error = undefined;
	};

	@action private receive = (items?: Array<IIdName>, error?: string) => {
		this.isLoading = false;
		this.items = items ?? this.items;
		this.error = error;
	};
}

export default new Store();
