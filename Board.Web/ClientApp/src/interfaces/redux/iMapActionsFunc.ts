import { Action, Dispatch } from "redux";

export default interface IMapDispatchToProps<TDispatchProps> {
	(dispatch: Dispatch<Action>): TDispatchProps;
}
