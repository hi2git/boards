import React from "react";
import { observer } from "mobx-react";

import { LoadablePanelFull, FileSelect } from "../common";

import store from "../../reducers/boardItems";

import Toggler from "./contentBoardControl";
import Scale from "./contentBoardScale";
import ContentTable from "./contentBoardTable";
import ViewBtns from "./contentBoardViews";

const item = { id: "", isDone: false, orderNumber: store.items.length };

interface IProps {}

const Board: React.FC<IProps> = () => {
	const { isLoading, error } = store;

	React.useEffect(() => {
		store.fetchAll();
	}, []);

	return (
		<>
			<div className="row mt-2">
				<div className="col-12">
					<FileSelect title="Добавить пост" item={item} onChange={store.post} isAdd />
					<Toggler />

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
