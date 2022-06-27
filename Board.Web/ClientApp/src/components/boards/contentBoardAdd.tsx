import { observer } from "mobx-react";
import React from "react";

import { FileSelect } from "../common";
import posts from "../../reducers/posts";

interface IProps {}

const AddBtn: React.FC<IProps> = () => {
	const addItem = { id: "", isDone: false, orderNumber: posts.items.length };
	return <FileSelect title="Добавить пост" item={addItem} onChange={posts.post} isAdd />;
};

export default observer(AddBtn);
