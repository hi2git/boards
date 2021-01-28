import { AnyAction } from "redux";

import { ILoginReducer } from "../interfaces/reducers";
import * as types from "../types/login";

const initialState: ILoginReducer = {
	// items: [],
	isLoading: false, // !!!! DEFAULT = FALSE
	error: undefined,
};

const reducer = (state = initialState, action: AnyAction) => {
	switch (action.type) {
		case types.REQUEST:
			return {
				...state,
				// items: [],
				isLoading: true,
				error: undefined,
			};
		case types.RECEIVE:
			return {
				...state,
				// items: action.payload,
				isLoading: false,
				error: undefined,
			};
		case types.ERROR:
			return {
				...state,
				// items: [],
				isLoading: false,
				error: action.payload,
			};
		default:
			return state;
	}
};

export default reducer;
