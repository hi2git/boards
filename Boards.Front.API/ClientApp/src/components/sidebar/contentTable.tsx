import { observer } from "mobx-react";
import React from "react";

import boards from "../../reducers/boards";

import Row from "./contentTableRow";

interface IProps {}

const Table: React.FC<IProps> = () => {
	const rows = boards.items.map(n => <Row key={n.id} item={n} />);
	return (
		<table className="table mt-2">
			<tbody>{rows}</tbody>
		</table>
	);
};

export default observer(Table);
