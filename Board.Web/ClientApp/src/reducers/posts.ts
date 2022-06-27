import { action, makeAutoObservable, observable } from "mobx";
import { IPost } from "../interfaces/components";

import board from "./board";

import service from "../services/posts";

class Posts {
	constructor() {
		makeAutoObservable(this);
	}

	@observable items: Array<IPost> = [];
	@observable isLoading: boolean = false;
	@observable error: string | undefined = undefined;

	@action set = (item: IPost) => (this.items = this.items.map(n => (n.id === item.id ? item : n)));

	@action clear = () => this.request();

	@action fetchAll = async () => {
		this.request();
		try {
			this.receive(await service.getAll(board.value?.id));
		} catch (e) {
			this.receive(undefined, e);
		}
	};
	@action sort = async (items: Array<IPost>) => {
		await service.sort(items, board.value?.id);
		await this.fetchAll();
	};
	@action post = async (item: IPost) => {
		await service.post(item, board.value?.id);
		await this.fetchAll();
	};
	@action put = async (item: IPost) => {
		const isContentChanged = this.items.find(n => n.id === item.id)?.content !== item.content;
		this.set(item);
		const action = isContentChanged ? this.putContent : this.putDescription;
		await action(item);
	};
	@action del = async (id: string) => {
		await service.del(id, board.value?.id);
		await this.fetchAll();
	};

	@action private putDescription = async (item: IPost) => {
		await service.put(item, board.value?.id);
	};
	@action private putContent = async (item: IPost) => {
		await service.putContent(item, board.value?.id);
	};

	@action private request = () => {
		this.isLoading = true;
		this.items = [];
		this.error = undefined;
	};
	@action private receive = (items?: Array<IPost>, error?: string) => {
		this.isLoading = false;
		this.items = items ?? [];
		this.error = error;
	};
}

export default new Posts();
