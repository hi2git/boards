import React from "react";
import { observer } from "mobx-react";

import store from "../../reducers/settings";
import { AlertDanger, LoadablePanel } from "../common";

import Form from "./contentForm";

interface IProps {}

const Content: React.FC<IProps> = () => {
	const { isLoading, error, reload } = store;

	React.useEffect(() => {
		reload();
	}, [reload]);

	return (
		<div className="settings">
			<div className="row mt-2">
				<div className="offset-1 col-6">
					<LoadablePanel isLoading={isLoading}>
						<AlertDanger value={error} />
						<Form />
					</LoadablePanel>
				</div>
			</div>
		</div>
	);
};

export default observer(Content);
