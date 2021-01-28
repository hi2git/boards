import { Action, Reducer } from "redux";
import { RouterState } from "connected-react-router";
import { LocationState } from "history";

import { IBoardItemsReducer, ILoginReducer, IUsersReducer, IViewReducer, IViewsReducer } from ".";

export default interface IRootReducer {
	boardItems: IBoardItemsReducer;
	users: IUsersReducer;
	view: IViewReducer;
	views: IViewsReducer;
	login: ILoginReducer;

	router: RouterState<LocationState>;
}

type RootReducer = IRootReducer;

export type IRootReducerFunc<T extends Action> = {
	[K in keyof RootReducer]: Reducer<RootReducer[K], T>;
};
