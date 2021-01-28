import { Dispatch, Action } from "redux";
import { push } from "connected-react-router";

import { IUserLogin } from "../interfaces/components";
import * as types from "../types/login";
import Service from "../services/login";

const service = new Service();

export const post = (item: IUserLogin) => async (dispatch: Dispatch<Action>) => {
	await dispatch({ type: types.REQUEST });
	try {
		await service.post(item);
		await dispatch({ type: types.RECEIVE });
		await dispatch(push("/"));
	} catch (e) {
		await dispatch({ type: types.ERROR, payload: e });
	}
};

export const logout = () => async (dispatch: Dispatch<Action>) => {
	await dispatch({ type: types.REQUEST });
	try {
		await service.delete();
		await dispatch({ type: types.RECEIVE });
		await dispatch(push("/login"));
	} catch (e) {
		await dispatch({ type: types.ERROR, payload: e });
	}
};
