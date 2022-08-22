import React from "react";
import { observer } from "mobx-react";
import { useResizeDetector } from "react-resize-detector";

import { LoadablePanelFull, Paging } from "../common";

import posts from "../../reducers/posts";

import Sidebar from "./contentBoardSideBar";
import Add from "./contentBoardAdd";
import Control from "./contentBoardControl";
import Palette from "./contentBoardPalette";
import Scale from "./contentBoardScale";
import ContentTable from "./contentBoardTable";
import ViewBtns from "./contentBoardViews";
import board from "../../reducers/board";
import { IPostFilter } from "../../interfaces/components";

interface IProps {}

const Board: React.FC<IProps> = () => {
	const { value } = board;
	const { width, ref } = useResizeDetector<HTMLDivElement>();

	const { filter, total, isLoading, error, fetchAll, initFilter, updateFilter } = posts;

	React.useEffect(() => {
		if (!value) return;
		initFilter(value.id);
		fetchAll();
	}, [value]);

	const search = (fltr: Partial<IPostFilter>) => {
		Object.entries(fltr).forEach(n => updateFilter(n[0] as keyof IPostFilter, n[1]));
		return fetchAll();
	};

	return !value ? null : (
		<LoadablePanelFull isLoading={isLoading} error={error}>
			<div className="row">
				<div className="col-xl-3 mt-2">
					<Sidebar />
					<Add />
					<Control />
					<Palette />
				</div>
				<div className="col mt-2 d-flex justify-xl-content-center">
					<Paging
						defaultCurrent={filter.index}
						pageSize={filter.size}
						total={total}
						onShowSizeChange={(index, size) => search({ index, size })}
						onChange={index => search({ index })}
					/>
				</div>
				<div className="col-lg-4 mt-2 d-flex justify-xl-content-end">
					<Scale />
					<ViewBtns />
				</div>
			</div>
			<div className="row mt-2">
				<div ref={ref} className="col-12">
					<ContentTable width={width} />
				</div>
			</div>
		</LoadablePanelFull>
	);
};

export default observer(Board);
