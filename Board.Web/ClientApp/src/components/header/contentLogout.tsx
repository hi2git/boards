import React from "react";
import { connect } from "react-redux";

import { IMapActionFunc } from "../../interfaces/redux";
import { MenuItem } from "../common";
import * as actions from "../../actions/login";

interface IDispatchProps {
	logout: () => Promise<void>;
}

interface IProps extends IDispatchProps {}

const Logout: React.FC<IProps> = ({ logout, ...props }) => {
	return (
		<MenuItem title="Выйти" className="float-right mr-0" {...props} onClick={logout}>
			Выйти <i className="fas fa-sign-out-alt ml-1" />
		</MenuItem>
	);
};

const mapActions: IMapActionFunc<IDispatchProps> = dispatch => {
	return {
		logout: () => actions.logout()(dispatch),
	};
};

export default connect(null, mapActions)(Logout);
