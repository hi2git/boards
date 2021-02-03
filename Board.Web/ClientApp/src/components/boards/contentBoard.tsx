import React from "react";
import { observer } from "mobx-react";

import { LoadablePanelFull, FileSelect } from "../common";

import boardItems from "../../reducers/boardItems";

import Sidebar from "./contentBoardSideBar";
import Add from "./contentBoardAdd";
import Control from "./contentBoardControl";
import Palette from "./contentBoardPalette";
import Scale from "./contentBoardScale";
import ContentTable from "./contentBoardTable";
import ViewBtns from "./contentBoardViews";

interface IProps {}

const Board: React.FC<IProps> = () => {
	const { isLoading, error } = boardItems;

	return (
		<>
			<div className="row mt-2">
				<div className="col-12">
					<Sidebar />
					<Add />
					<Control />
					<Palette />

					<ViewBtns />
					<Scale />
				</div>
			</div>
			<div className="row mt-1">
				<div className="col-12">
					<LoadablePanelFull isLoading={isLoading} error={error}>
						<ContentTable />
					</LoadablePanelFull>
				</div>
			</div>
		</>
	);
};

export default observer(Board);
