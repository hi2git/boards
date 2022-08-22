import React from "react";
import { observer } from "mobx-react";
import { Empty } from "../common";

import Add from "./contentBoardAdd";

interface IProps {}

const NoneResults: React.FC<IProps> = () => (
	<>
		<div className="row mt-2">
			<div className="col-12">
				<Empty description="У тебя пока нет ни одного поста" />
			</div>
		</div>
		<div className="row mt-2">
			<div className="col-12 d-flex justify-content-center">
				<p>Для добавления новых постов нажми</p>
				<Add className="ml-2" />
			</div>
		</div>
	</>
);

export default observer(NoneResults);
