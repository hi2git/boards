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
			this.receive(await service.getAll(board.value?.id)); // TODO use cookie to store board.id
		} catch (e) {
			this.receive(undefined, e);
		}
	};

	@action sort = (items: Array<IPost>) => this.handle(() => service.sort(items, board.value?.id)); // TODO use cookie to store board.id

	@action post = (item: IPost) => this.handle(() => service.post(item, board.value?.id)); // TODO use cookie to store board.id

	@action put = (item: IPost) =>
		this.handle(async () => {
			const isContentChanged = this.items.find(n => n.id === item.id)?.content !== item.content;
			this.set(item);
			const action = isContentChanged ? this.putContent : this.putDescription;
			await action(item);
		});

	@action del = (id: string) => this.handle(() => service.del(id, board.value?.id)); // TODO use cookie to store board.id

	private putDescription = (item: IPost) => service.put(item, board.value?.id); // TODO use cookie to store board.id

	private putContent = (item: IPost) => service.putContent(item, board.value?.id); // TODO use cookie to store board.id

	private handle = async (action: () => Promise<void>) => {
		this.request();
		try {
			await action();
			await this.fetchAll();
		} catch (e) {
			this.receive(undefined, e);
		}
	};

	private request = () => {
		this.isLoading = true;
		this.error = undefined;
	};
	private receive = (items?: Array<IPost>, error?: any) => {
		this.isLoading = false;
		this.items = items ?? this.items;
		this.error = error as string;
	};
}

export default new Posts();
