import React from "react";
import { observer } from "mobx-react";

import { IIdName } from "../../interfaces/components";
import { Button, confirm } from "../common";
import boards from "../../reducers/boards";
import board from "../../reducers/board";

import ValueCell from "./contentTableRowCell";
import EditSaveBtn from "./contentTableRowEdit";

interface IProps {
	item: IIdName;
}

const Row: React.FC<IProps> = ({ item }) => {
	const [isEdit, setEdit] = React.useState(false); // store.put(item)
	const [name, setName] = React.useState(item.name);

	const newItem = { ...item, name };
	const submit = async () => {
		setEdit(false);
		await boards.put(newItem);
	};

	const cancel = async () => {
		setEdit(false);
		await setName(item.name);
	};

	const del = async () => confirm({ title: "Подтвердите удаление", onOk: () => boards.del(newItem.id) });

	const borderCls = board.value?.id === item.id ? "background-blue-light" : undefined;

	return (
		<tr className={`row ${borderCls}`}>
			<td className="col-8">
				<ValueCell isEdit={isEdit} item={newItem} onChange={setName} onSubmit={submit} />
			</td>
			<td className="col-4 pl-0 text-right">
				<EditSaveBtn
					isEdit={isEdit}
					isDisabled={!name}
					onStartEdit={() => setEdit(true)}
					onCancel={cancel}
					onSubmit={submit}
				/>
				<Button
					className="px-0"
					title={boards.deleteTitle}
					type="link"
					onClick={del}
					disabled={!boards.canDelete}
				>
					<i className="fas fa-times" />
				</Button>
			</td>
		</tr>
	);
};

export default observer(Row);
