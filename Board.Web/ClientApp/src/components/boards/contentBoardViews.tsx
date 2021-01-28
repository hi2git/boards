import React, { useEffect } from "react";
import { IIdName } from "../../interfaces/components";
import { RadioGroup } from "../common";

import * as viewActions from "../../actions/view";
import * as viewsActions from "../../actions/views";
import { IMapStateFunc, IMergeFunc } from "../../interfaces/redux";
import { connect } from "react-redux";

interface IOwnProps {}

interface IStateToProps extends IOwnProps {
	items: Array<IIdName>;
	isLoading: boolean;
}

interface IDispatchProps {
	fetchAll: () => Promise<void>;
	set: (id: string) => Promise<void>;
}

interface IProps extends IStateToProps, IDispatchProps {}

const Views: React.FC<IProps> = ({ items, isLoading, set, fetchAll }) => {
	useEffect(() => {
		fetchAll();
	}, []);

	if (isLoading) return null;

	const btns = [
		{ view: items.find(n => n.id === "cover") as IIdName, icon: "arrows-alt" },
		{ view: items.find(n => n.id === "fill") as IIdName, icon: "arrows-alt-h" },
		{ view: items.find(n => n.id === "scale-down") as IIdName, icon: "arrows-alt-v" },
	];

	const opts =
		btns.map(n => ({
			id: n.view.id,
			title: n.view.name,
			label: <i className={`fas fa-${n.icon}`} />,
		})) ?? [];

	return <RadioGroup className="float-right" opts={opts} onChange={e => set(e.target.value)} />;
};

const mapStateToProps: IMapStateFunc<IStateToProps, IOwnProps> = ({ views }, ownProps) => {
	const { items, isLoading } = views;
	return { ...ownProps, items, isLoading };
};

const mergeProps: IMergeFunc<IProps, IStateToProps, IOwnProps> = (stateProps, { dispatch }, ownProps) => {
	return {
		...ownProps,
		...stateProps,
		set: name => viewActions.set(name)(dispatch),
		fetchAll: () => viewsActions.fetchAll()(dispatch),
	};
};

export default connect(mapStateToProps, null, mergeProps)(Views);
