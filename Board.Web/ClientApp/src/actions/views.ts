import { Action, Dispatch } from "redux";

import * as types from "../types/views";

import Service from "../services/views";

const service = new Service();

export const fetchAll = () => async (dispatch: Dispatch<Action>) => {
	await dispatch({ type: types.REQUEST });
	try {
		const items = await service.getAll();
		await dispatch({ type: types.RECEIVE, payload: items });
	} catch (e) {
		await dispatch({ type: types.ERROR, payload: e.message });
	}
};
