import { Action, Dispatch } from "redux";
import * as types from "../types/view";

export const set = (name: string) => async (dispatch: Dispatch<Action>) => {
	await dispatch({ type: types.RECEIVE, payload: name });
};
