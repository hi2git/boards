import { Action, Dispatch } from "redux";

import * as types from "../types/users";
import Service from "../services/users";

const service = new Service();

export const fetchAll = () => async (dispatch: Dispatch<Action>) => {
	await dispatch({ type: types.REQUEST_USERS });
	try {
		const items = await service.getAll();
		await dispatch({ type: types.RECEIVE_USERS, payload: items });
	} catch (ex) {
		await dispatch({ type: types.ERROR_USERS, payload: ex.message });
	}
};
