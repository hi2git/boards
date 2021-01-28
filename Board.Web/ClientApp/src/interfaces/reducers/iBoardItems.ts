import { IBoardItem } from "../components";

export default interface IBoardItemsReducer {
	items: Array<IBoardItem>;
	isLoading: boolean;
	error?: string;
}
