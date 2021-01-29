import React, { Component } from "react";
import { connect } from "react-redux";

import { IBoardItem } from "../../interfaces/components";
import { IMapStateFunc, IMergeFunc } from "../../interfaces/redux";
import { LoadablePanel, LoadablePanelFull, FileSelect } from "../common";

import * as actions from "../../actions/boards";

import Toggler from "./contentBoardToggler";
import ContentTable from "./contentBoardTable";
import ViewBtns from "./contentBoardViews";

interface IOwnProps {}

interface IStateToProps extends IOwnProps {
	items: Array<IBoardItem>;
	isLoading: boolean;
	error?: string;
}

interface IDispatchProps {
	fetch: () => Promise<void>;
	sort: (items: Array<IBoardItem>) => Promise<void>;
	post: (item: IBoardItem) => Promise<void>;
	putContent: (item: IBoardItem) => Promise<void>;
	put: (item: IBoardItem) => Promise<void>;
	delete: (id: string) => Promise<void>;
}

interface IProps extends IStateToProps, IDispatchProps {}

interface IState {
	items: Array<IBoardItem>;
}

export class Board extends Component<IProps, IState> {
	state: IState = {
		items: [],
	};

	componentDidMount = async () => this.reload();

	componentDidUpdate = (prev: IProps) => {
		if (prev.items === this.props.items || !this.props.items) return;
		this.setState({ items: [...this.props.items] });
	};

	reload = async () => this.props.fetch();

	change = async (item: IBoardItem) => {
		const items = this.state.items.map(n => (n.id === item.id ? item : n));
		const isContentChanged = this.state.items.find(n => n.id === item.id)?.content !== item.content;
		this.setState({ items });
		const action = isContentChanged ? this.props.putContent : this.props.put;
		await action(item);
		// this.reload();
	};

	add = async (item: IBoardItem) => {
		await this.props.post(item);
		this.reload();
	};

	sort = async (items: Array<IBoardItem>) => {
		await this.props.sort(items);
		// this.setState({ items });
		this.reload();
	};

	delete = async (id: string) => {
		await this.props.delete(id);
		this.reload();
	};

	render = () => {
		const { isLoading, error } = this.props;
		const { items } = this.state;

		const item = { id: "", isDone: false, orderNumber: this.state.items.length };

		return (
			<>
				<div className="row mt-2">
					<div className="col-12">
						<FileSelect title="Добавить пост" item={item} onChange={this.add} isAdd />
						<Toggler />
						<ViewBtns />
					</div>
				</div>
				<div className="row mt-1">
					<div className="col-12">
						<LoadablePanelFull isLoading={isLoading} error={error}>
							<ContentTable
								items={items}
								onChange={this.change}
								onSorted={this.sort}
								onDelete={this.delete}
							/>
						</LoadablePanelFull>
					</div>
				</div>
			</>
		);
	};
}

const mapStateToProps: IMapStateFunc<IStateToProps, IOwnProps> = ({ boardItems, views }, ownProps) => {
	const { items, isLoading, error } = boardItems;
	return {
		...ownProps,
		items,
		isLoading,
		error,
	};
};

const mergeProps: IMergeFunc<IProps, IStateToProps, IOwnProps> = (stateProps, { dispatch }, ownProps) => {
	return {
		...ownProps,
		...stateProps,
		fetch: () => actions.fetchAll()(dispatch),
		sort: items => actions.sort(items)(dispatch),
		post: item => actions.post(item)(dispatch),
		putContent: item => actions.putContent(item)(dispatch),
		put: item => actions.put(item)(dispatch),
		delete: id => actions.del(id)(dispatch),
	};
};

export default connect(mapStateToProps, null, mergeProps)(Board);
