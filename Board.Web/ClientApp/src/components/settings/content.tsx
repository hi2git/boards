import React from "react";
import { observer } from "mobx-react";

import settings from "../../reducers/settings";
import { AlertDanger, LoadablePanel } from "../common";

import Form from "./contentForm";

interface IProps {}

const Content: React.FC<IProps> = () => {
	const { isLoading, error, reload } = settings;

	React.useEffect(() => {
		reload();
	}, []);

	return (
		<div className="settings">
			<LoadablePanel isLoading={isLoading}>
				<AlertDanger value={error} />
				<Form />
			</LoadablePanel>
		</div>
	);
};

export default observer(Content);
