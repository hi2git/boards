import React from "react";

import { IBoardItem } from "../../interfaces/components";

import Image from "./contentBoardTableItemImg";

interface IProps {
	item: IBoardItem;
	height: number;
	onChange: (item: IBoardItem) => void;
	onDelete?: (id: string) => void;
}

const Container: React.FC<IProps> = ({ item, height, onChange, onDelete }) => (
	<Image key={item.id} item={item} height={height} onChange={onChange} onDelete={onDelete} />
);

export default Container;
