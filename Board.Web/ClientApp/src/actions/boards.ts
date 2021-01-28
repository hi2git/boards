import { Action, Dispatch } from "redux";

import * as types from "../types/boardItems";
import Service from "../services/boardItems";
import { IBoardItem } from "../interfaces/components";

const service = new Service();

export const fetchAll = () => async (dispatch: Dispatch<Action>) => {
	await dispatch({ type: types.REQUEST_BOARD_ITEMS });
	try {
		const items = await service.getAll();
		await dispatch({ type: types.RECEIVE_BOARD_ITEMS, payload: items });
	} catch (ex) {
		await dispatch({ type: types.ERROR_BOARD_ITEMS, payload: ex.message });
	}
};

export const sort = (items: Array<IBoardItem>) => async (dispatch: Dispatch<Action>) => {
	// await dispatch({ type: types.REQUEST_BOARD_ITEMS });
	// try {
	await service.sort(items);
	// 	await dispatch({ type: types.RECEIVE_BOARD_ITEMS });
	// } catch (ex) {
	// 	await dispatch({ type: types.ERROR_BOARD_ITEMS, payload: ex.message });
	// }
};

export const post = (item: IBoardItem) => async (dispatch: Dispatch<Action>) => {
	// await dispatch({ type: types.REQUEST_BOARD_ITEMS });
	// try {
	await service.post(item);
	// 	await dispatch({ type: types.RECEIVE_BOARD_ITEMS });
	// } catch (ex) {
	// 	await dispatch({ type: types.ERROR_BOARD_ITEMS, payload: ex.message });
	// }
};
export const putContent = (item: IBoardItem) => async (dispatch: Dispatch<Action>) => {
	// await dispatch({ type: types.REQUEST_BOARD_ITEMS });
	// try {
	await service.putContent(item);
	// 	await dispatch({ type: types.RECEIVE_BOARD_ITEMS });
	// } catch (ex) {
	// 	await dispatch({ type: types.ERROR_BOARD_ITEMS, payload: ex.message });
	// }
};

export const put = (item: IBoardItem) => async (dispatch: Dispatch<Action>) => {
	// await dispatch({ type: types.REQUEST_BOARD_ITEMS });
	// try {
	await service.put(item);
	// 	await dispatch({ type: types.RECEIVE_BOARD_ITEMS });
	// } catch (ex) {
	// 	await dispatch({ type: types.ERROR_BOARD_ITEMS, payload: ex.message });
	// }
};

export const del = (id: string) => async (dispatch: Dispatch<Action>) => {
	// await dispatch({ type: types.REQUEST_BOARD_ITEMS });
	// try {
	await service.del(id);
	// 	await dispatch({ type: types.RECEIVE_BOARD_ITEMS });
	// } catch (ex) {
	// 	await dispatch({ type: types.ERROR_BOARD_ITEMS, payload: ex.message });
	// }
};
