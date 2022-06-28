import { observer } from "mobx-react";
import React from "react";

import Add from "./contentBoardAdd";

interface IProps {}

const NoneResults: React.FC<IProps> = () => (
	<div className="row mt-2">
		<div className="col-12">
			Для добавления новых постов нажмите <Add />
		</div>
	</div>
);

export default observer(NoneResults);
