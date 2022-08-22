import React from "react";
import { observer } from "mobx-react";

import { AlertDanger, Drawer, LoadablePanel } from "../common";
import sidebar from "../../reducers/boardSidebar";
import boards from "../../reducers/boards";

import AddForm from "./contentForm";
import Table from "./contentTable";

import "./content.css";

interface IProps {}

const SideContent: React.FC<IProps> = () => {
	const { value, toggle } = sidebar;
	const { isLoading, error } = boards;

	return (
		<Drawer title="Доски" className="sidebar" visible={value} onClose={toggle}>
			<LoadablePanel isLoading={isLoading}>
				<AlertDanger value={error} />
				<AddForm />
				<Table />
			</LoadablePanel>
		</Drawer>
	);
};

export default observer(SideContent);
