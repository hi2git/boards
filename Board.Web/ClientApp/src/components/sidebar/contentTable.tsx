import { observer } from "mobx-react";
import React from "react";

import boards from "../../reducers/boards";

import Row from "./contentTableRow";

// const items = [
// 	{ id: "01", name: "Arbol" },
// 	{ id: "02", name: "Mouse12x3oz" },
// ];

interface IProps {}

const Table: React.FC<IProps> = () => {
	const rows = boards.items.map(n => <Row item={n} />);
	return (
		<table className="table table- mt-4">
			<tbody>{rows}</tbody>
		</table>
	);
};

export default observer(Table);
