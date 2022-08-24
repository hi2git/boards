import React, { useEffect } from "react";
import board from "../../reducers/board";

interface IProps {}

const Content: React.FC<IProps> = () => {
	const { clear } = board;
	useEffect(() => {
		clear();
	}, []);

	return (
		<div className="contacts row mt-2">
			<div className="col-12">Страница наполняется</div>
		</div>
	);
};

export default Content;
