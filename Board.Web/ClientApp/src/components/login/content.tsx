import React from "react";
import { connect } from "react-redux";

import { AlertDanger, LoadablePanel } from "../common";
import { IUserLogin } from "../../interfaces/components";
import { IMapActionFunc, IMapStateFunc } from "../../interfaces/redux";
import * as actions from "../../actions/login";

import ContentForm from "./contentForm";

interface IStateToProps {
	isLoading: boolean;
	error?: string;
}

interface IDispatchProps {
	post: (item: IUserLogin) => Promise<void>;
}

interface IProps extends IStateToProps, IDispatchProps {}

const Content: React.FC<IProps> = ({ isLoading, error, post }) => {
	console.log(error);
	return (
		<LoadablePanel isLoading={isLoading}>
			<AlertDanger value={error} />
			<ContentForm onOk={post} />
		</LoadablePanel>
	);
};

const mapStateToProps: IMapStateFunc<IStateToProps> = ({ login }) => {
	const { isLoading, error } = login;
	return { isLoading, error };
};

const mapActions: IMapActionFunc<IDispatchProps> = dispatch => {
	return {
		post: item => actions.post(item)(dispatch),
	};
};

export default connect(mapStateToProps, mapActions)(Content);
