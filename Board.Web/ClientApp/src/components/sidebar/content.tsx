import React from "react";
import { observer } from "mobx-react";

import { Drawer, LoadablePanel } from "../common";
import sidebar from "../../reducers/boardSidebar";
import boards from "../../reducers/boards";

import AddForm from "./contentForm";
import Table from "./contentTable";

interface IProps {}

const SideContent: React.FC<IProps> = () => {
	const { value, toggle } = sidebar;
	const { isLoading, error } = boards;

	return (
		<Drawer visible={value} onClose={toggle}>
			<h2>Доски</h2>
			<LoadablePanel isLoading={isLoading} error={error}>
				<AddForm />
				<Table />
			</LoadablePanel>
		</Drawer>
	);
};

export default observer(SideContent);
