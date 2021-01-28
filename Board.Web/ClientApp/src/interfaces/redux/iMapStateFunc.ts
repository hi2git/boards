import { IRootReducer } from "../reducers";

import { MapStateToProps } from "react-redux";

export default interface IMapStateFunc<TOutProps, TOwnProps>
	extends MapStateToProps<TOutProps, TOwnProps, IRootReducer> {}
