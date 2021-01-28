import MapStateFunc from "./iMapStateFunc";
import MapActionFunc from "./iMapActionsFunc";
import MergeFunc from "./iMergeFunc";

export type IMapStateFunc<TStateToProps, TOwnProps = {}> = MapStateFunc<TStateToProps, TOwnProps>;
export type IMapActionFunc<TDispatchProps> = MapActionFunc<TDispatchProps>;
export type IMergeFunc<TOutProps, TStateProps, TOwnProps> = MergeFunc<TOutProps, TStateProps, TOwnProps>;
