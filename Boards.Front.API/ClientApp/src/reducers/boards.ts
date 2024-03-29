import { action, computed, makeAutoObservable, observable } from "mobx";

import { IIdName } from "../interfaces/components";
import service from "../services/boards";
import board from "./board";
import posts from "./posts";

class Store {
	constructor() {
		makeAutoObservable(this);
	}

	@observable items: Array<IIdName> = [];
	@observable isLoading: boolean = true;
	@observable error: string | undefined = undefined;

	@computed get canDelete() {
		return this.items.length > 1;
	}

	@computed get deleteTitle() {
		return this.canDelete ? "Удалить" : "Нельзя удалить последнюю доску";
	}

	@action clear = () => {
		this.items = [];
		this.request();
		posts.clear();
		board.clear();
	};

	@action fetchAll = async () => {
		this.request();
		try {
			this.receive(await service.getAll());
			await board.mount(this.first);
		} catch (e) {
			this.receive([], e);
		}
	};

	@action post = async (name: string) => {
		this.request();
		try {
			await service.post(name);
			await this.fetchAll();
			const item = this.getByName(name);
			await board.setValue(item);
		} catch (e) {
			this.receive(undefined, e);
		}
	};
	@action put = async (item: IIdName) => {
		this.request();
		try {
			await service.put(item);
			await this.fetchAll();
			const current = this.getByName(item.name);
			await board.setValue(current);
		} catch (e) {
			this.receive(undefined, e);
		}
	};
	@action del = async (id: string) => {
		this.request();
		try {
			await service.del(id);
			if (board.value?.id === id) await board.setValue(this.first);
			await this.fetchAll();
		} catch (e) {
			this.receive(undefined, e);
		}
	};

	@action private request = () => {
		this.isLoading = true;
		this.error = undefined;
	};

	@action private receive = (items?: Array<IIdName>, error?: any) => {
		this.isLoading = false;
		this.items = items ?? this.items;
		this.error = error;
	};

	@computed private get first() {
		return this.items[0];
	}

	public get = (id: string) => this.items?.find(n => n.id === id);
	public getByName = (name: string) => this.items?.find(n => n.name === name);
}

export default new Store();
