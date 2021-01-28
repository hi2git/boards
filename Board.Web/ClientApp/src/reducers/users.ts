import { AnyAction } from "redux";

import { IUsersReducer } from "../interfaces/reducers";
import * as types from "../types/users";

const initialState: IUsersReducer = {
	items: [],
	isLoading: true,
	error: undefined,
};

const reducer = (state = initialState, action: AnyAction) => {
	switch (action.type) {
		case types.REQUEST_USERS:
			return {
				...state,
				items: [],
				isLoading: true,
				error: undefined,
			};
		case types.RECEIVE_USERS:
			return {
				...state,
				items: action.payload,
				isLoading: false,
				error: undefined,
			};
		case types.ERROR_USERS:
			return {
				...state,
				items: [],
				isLoading: false,
				error: action.payload,
			};
		default:
			return state;
	}
};

export default reducer;
