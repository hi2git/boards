import React from "react";
import { connect } from "react-redux";

import { LoadablePanel } from "../common";
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
	return (
		<div className="row" style={{ marginTop: "30%" }}>
			<div className="offset-2 col-8">
				<LoadablePanel isLoading={isLoading} error={error}>
					<ContentForm onOk={post} />
				</LoadablePanel>
			</div>
		</div>
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
