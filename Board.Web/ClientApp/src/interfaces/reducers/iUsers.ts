import { IIdName } from "../components";

export default interface IUsersReducer {
	items: Array<IIdName>;
	isLoading: boolean;
	error?: string;
}
