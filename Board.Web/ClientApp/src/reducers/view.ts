import { AnyAction } from "redux";

import { IViewReducer } from "../interfaces/reducers";
import * as types from "../types/view";

const initialState: IViewReducer = {
	item: "cover",
};

const reducer = (state = initialState, action: AnyAction) => {
	switch (action.type) {
		// case types.REQUEST_USERS:
		// 	return {
		// 		...state,
		// 		items: [],
		// 		isLoading: true,
		// 		error: undefined,
		// 	};
		case types.RECEIVE:
			return {
				...state,
				item: action.payload,
			};
		// case types.ERROR_USERS:
		// 	return {
		// 		...state,
		// 		items: [],
		// 		isLoading: false,
		// 		error: action.payload,
		// 	};
		default:
			return state;
	}
};

export default reducer;
