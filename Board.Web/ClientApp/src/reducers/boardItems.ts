import { action, makeAutoObservable, observable } from "mobx";
import { IBoardItem } from "../interfaces/components";

import board from "./board";

import service from "../services/boardItems";

class BoardItems {
	constructor() {
		makeAutoObservable(this);
	}

	@observable items: Array<IBoardItem> = [];
	@observable isLoading: boolean = false;
	@observable error: string | undefined = undefined;

	@action set = (item: IBoardItem) => (this.items = this.items.map(n => (n.id === item.id ? item : n)));

	@action clear = () => this.request();

	@action fetchAll = async () => {
		this.request();
		try {
			this.receive(await service.getAll(board.value as string));
		} catch (e) {
			this.setError(e);
		}
	};
	@action sort = async (items: Array<IBoardItem>) => {
		await service.sort(board.value as string, items);
		await this.fetchAll();
	};
	@action post = async (item: IBoardItem) => {
		await service.post(board.value as string, item);
		await this.fetchAll();
	};
	@action put = async (item: IBoardItem) => {
		const isContentChanged = this.items.find(n => n.id === item.id)?.content !== item.content;
		this.set(item);
		const action = isContentChanged ? this.putContent : this.putDescription;
		await action(item);
	};
	@action del = async (id: string) => {
		await service.del(id);
		await this.fetchAll();
	};

	@action private putDescription = async (item: IBoardItem) => {
		await service.put(item);
	};
	@action private putContent = async (item: IBoardItem) => {
		await service.putContent(item);
	};

	@action private request = () => {
		this.isLoading = true;
		this.items = [];
		this.error = undefined;
	};
	@action private receive = (items: Array<IBoardItem>) => {
		this.isLoading = false;
		this.items = [...items];
		this.error = undefined;
	};
	@action private setError = (value: string) => {
		this.isLoading = false;
		this.items = [];
		this.error = value;
	};
}

export default new BoardItems();
