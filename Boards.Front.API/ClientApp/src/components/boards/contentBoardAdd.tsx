import { observer } from "mobx-react";
import React from "react";

import { FileSelect } from "../common";
import posts from "../../reducers/posts";

interface IProps {
	className?: string;
}

const AddBtn: React.FC<IProps> = ({ className }) => {
	const addItem = { id: "", isDone: false, orderNumber: posts.items.length };
	return <FileSelect title="Добавить пост" item={addItem} className={className} onChange={posts.post} isAdd />;
};

export default observer(AddBtn);
