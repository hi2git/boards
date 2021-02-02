import React from "react";

import Board from "./contentBoard";
import Sidebar from "../sidebar/content";

import boards from "../../reducers/boards";
import { LoadablePanel } from "../common";

const Content: React.FC = () => {
	const { isLoading, error, fetchAll } = boards;
	React.useEffect(() => {
		fetchAll();
	}, [fetchAll]);

	return (
		<div className="boards">
			<Sidebar />
			<LoadablePanel isLoading={isLoading} error={error}>
				<Board />
			</LoadablePanel>
		</div>
	);
};

export default Content;
