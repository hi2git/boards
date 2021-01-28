import { MergeProps } from "react-redux";

export default interface IMergeFunc<TOutProps, TStateProps, TOwnProps>
	extends MergeProps<TStateProps, any, TOwnProps, TOutProps> {}
