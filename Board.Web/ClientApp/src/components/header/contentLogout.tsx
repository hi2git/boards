import React from "react";
import { observer } from "mobx-react";

import { LoadablePanel, MenuItem } from "../common";
import store from "../../reducers/login";

interface IProps {}

const Logout: React.FC<IProps> = props => {
	return (
		<MenuItem title="Выйти" className="float-right mr-0" {...props} onClick={store.logout}>
			<LoadablePanel isLoading={store.isLoading}>
				Выйти <i className="fas fa-sign-out-alt ml-1" />
			</LoadablePanel>
		</MenuItem>
	);
};

export default observer(Logout);
