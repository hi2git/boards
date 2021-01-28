import { connectRouter, RouterState } from "connected-react-router";
import { History, LocationState } from "history";
import { combineReducers, Reducer } from "redux";

import { IRootReducer, IRootReducerFunc } from "../interfaces/reducers";
import boardItems from "./boardItems";
import users from "./users";
import view from "./view";
import views from "./views";
import login from "./login";

interface IReducerBuilderFunc {
	(history: History<LocationState>): IRootReducerFunc;
}

const reducersBuilder: IReducerBuilderFunc = history => ({
	boardItems,
	users,
	view,
	views,
	login,

	router: connectRouter(history) as Reducer<RouterState>,
});

const createReducers = (history: History) => ({
	...reducersBuilder(history),
});

// eslint-disable-next-line import/no-anonymous-default-export
export default (history: History) => combineReducers<IRootReducer>(createReducers(history));
