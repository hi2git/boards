import { applyMiddleware, compose, createStore } from "redux";
import thunk from "redux-thunk";
import { routerMiddleware } from "connected-react-router";
import { History } from "history";

import createRootReducer from "../reducers/root";

// eslint-disable-next-line import/no-anonymous-default-export
export default (history: History, initialState: any) => {
	const middleware = [routerMiddleware(history), thunk]; // routerMiddleware(history),

	// In development, use the browser's Redux dev tools extension if installed
	const enhancers = new Array<any>();
	// const isDevelopment = process.env.NODE_ENV === "development";

	// if (isDevelopment && typeof window !== "undefined" && window.devToolsExtension) {
	//     enhancers.push(window.devToolsExtension());
	// }

	return createStore(
		//rootReducer,
		createRootReducer(history),
		initialState,
		compose(applyMiddleware(...middleware), ...enhancers)
	);
};
