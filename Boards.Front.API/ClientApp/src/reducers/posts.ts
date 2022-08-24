import { action, makeAutoObservable, observable } from "mobx";
import { IPageable, IPost, IPostFilter } from "../interfaces/components";

import board from "./board";

import service from "../services/posts";
import router from "./router";

const DEFAULT_FILTER: IPostFilter = { boardId: "", index: 1, size: 6 };
class Posts {
	constructor() {
		makeAutoObservable(this);
	}

	@observable items: Array<IPost> = [];
	@observable total: number = 50;
	@observable filter: IPostFilter = { ...DEFAULT_FILTER };
	@observable isLoading: boolean = false;
	@observable error: string | undefined = undefined;

	@action set = (item: IPost) => (this.items = this.items.map(n => (n.id === item.id ? item : n)));

	@action clear = () => this.request();

	@action initFilter = (boardId: string) => {
		this.filter = {
			boardId, //router.getSearch("board") as string,
			index: (router.getSearch("index") as number) ?? DEFAULT_FILTER.index,
			size: (router.getSearch("size") as number) ?? DEFAULT_FILTER.size,
		};
	};

	@action updateFilter = (key: keyof IPostFilter, value?: string | number) => {
		this.filter = { ...(this.filter as IPostFilter), [key]: value };
		router.setSearch(key, value);
	};

	@action fetchAll = async () => {
		this.request();
		try {
			// const filter = { boardId, index: 0, size: 12 };
			// if (!this.filter) throw new Error("Filter is undefined");
			const page = await service.getAll(this.filter);
			this.receive(page); // TODO use cookie to store board.id
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

	private receive = (page?: IPageable<IPost>, error?: any) => {
		this.isLoading = false;
		this.items = page?.items ?? this.items;
		this.total = page?.total ?? this.total;
		this.error = error as string;
	};
}

export default new Posts();
