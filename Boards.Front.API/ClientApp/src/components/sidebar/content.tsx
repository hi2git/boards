import React, { useEffect } from "react";
import { observer } from "mobx-react";

import { AlertDanger, Drawer, LoadablePanel } from "../common";
import sidebar from "../../reducers/boardSidebar";
import boards from "../../reducers/boards";
import board from "../../reducers/board";

import AddForm from "./contentForm";
import Table from "./contentTable";

import "./content.css";

interface IProps {}

const SideContent: React.FC<IProps> = () => {
	const { value, toggle, close } = sidebar;
	const { isLoading, error } = boards;
	const { value: brd } = board;

	useEffect(() => {
		close();
	}, [brd, close]);

	return (
		<Drawer width="auto" title="Доски" className="sidebar" visible={value} onClose={toggle}>
			<LoadablePanel isLoading={isLoading}>
				<AlertDanger value={error} />
				<AddForm />
				<Table />
			</LoadablePanel>
		</Drawer>
	);
};

export default observer(SideContent);
