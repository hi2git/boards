import { Action } from "redux";
import RootReducer, { IRootReducerFunc as RootReducerFunc } from "./iRoot";

import BoardItemsReducer from "./iBoardItems";
import UsersReducer from "./iUsers";
import ViewReducer from "./iView";
import ViewsReducer from "./iViews";
import LoginReducer from "./iLogin";

export type IRootReducer = RootReducer;
export type IRootReducerFunc<T extends Action = Action> = RootReducerFunc<T>;

export type IBoardItemsReducer = BoardItemsReducer;
export type IUsersReducer = UsersReducer;
export type IViewReducer = ViewReducer;
export type IViewsReducer = ViewsReducer;
export type ILoginReducer = LoginReducer;
