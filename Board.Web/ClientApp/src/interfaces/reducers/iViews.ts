import { Property } from "csstype";
import { IIdName } from "../components";

export default interface IViewsReducer {
	items: Array<IIdName>;
	isLoading: boolean;
	error?: string;
}
