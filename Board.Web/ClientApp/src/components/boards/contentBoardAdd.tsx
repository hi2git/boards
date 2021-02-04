import { observer } from "mobx-react";
import React from "react";

import { FileSelect } from "../common";
import boardItems from "../../reducers/boardItems";

interface IProps {}

const AddBtn: React.FC<IProps> = () => {
	const addItem = { id: "", isDone: false, orderNumber: boardItems.items.length };
	return <FileSelect title="Добавить пост" item={addItem} onChange={boardItems.post} isAdd />;
};

export default observer(AddBtn);
