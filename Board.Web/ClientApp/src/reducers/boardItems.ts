import { AnyAction } from "redux";

import { IBoardItemsReducer } from "../interfaces/reducers";
import * as types from "../types/boardItems";

const initialState: IBoardItemsReducer = {
	items: [],
	isLoading: true,
	error: undefined,
};

const reducer = (state = initialState, action: AnyAction) => {
	switch (action.type) {
		case types.REQUEST_BOARD_ITEMS:
			return {
				...state,
				// items: [],
				isLoading: true,
				error: undefined,
			};
		case types.RECEIVE_BOARD_ITEMS:
			return {
				...state,
				items: action.payload ?? state.items,
				isLoading: false,
				error: undefined,
			};
		case types.ERROR_BOARD_ITEMS:
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
