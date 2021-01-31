import React from "react";
import { observer } from "mobx-react";

import { AlertDanger, LoadablePanel } from "../common";

import store from "../../reducers/login";

import ContentForm from "./contentForm";

interface IProps {}

const Content: React.FC<IProps> = (/*{ isLoading, error, post }*/) => {
	const { isLoading, error, login } = store;
	return (
		<LoadablePanel isLoading={isLoading}>
			<AlertDanger value={error} />
			<ContentForm onOk={login} />
		</LoadablePanel>
	);
};

export default observer(Content);
