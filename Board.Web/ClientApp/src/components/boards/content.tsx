import React from "react";

import Board from "./contentBoard";
import Sidebar from "../sidebar/content";

const Content: React.FC = () => (
	<div className="boards">
		<Sidebar />
		<Board />
	</div>
);

export default Content;
