import React from "react";

import { IIdName } from "../../interfaces/components";
import { Button, Input } from "../common";
import boards from "../../reducers/boards";
import board from "../../reducers/board";

import EditSaveBtn from "./contentTableRowEdit";

interface IProps {
	item: IIdName;
}

const Row: React.FC<IProps> = ({ item }) => {
	const [isEdit, setEdit] = React.useState(false); // store.put(item)
	const [name, setName] = React.useState(item.name);

	const newItem = { ...item, name };
	const submit = () => {
		setEdit(false);
		boards.put(newItem);
	};

	return (
		<tr>
			<td>
				<ValueCell isEdit={isEdit} item={newItem} onChange={setName} />
			</td>
			<td className="text-right">
				<EditSaveBtn isEdit={isEdit} onStartEdit={() => setEdit(true)} onSubmit={submit} />
				<Button title="Удалить" type="link" onClick={() => boards.del(newItem.id)}>
					<i className="fas fa-times" />
				</Button>
			</td>
		</tr>
	);
};

interface IValueCellProps {
	isEdit: boolean;
	item: IIdName;
	onChange: (value: string) => void;
}

const ValueCell: React.FC<IValueCellProps> = ({ isEdit, item, onChange }) =>
	isEdit ? (
		<Input value={item.name} onChange={e => onChange(e.target.value)} />
	) : (
		<Button title="Показать" type="link" onClick={() => board.setValue(item.id)}>
			{item.name}
		</Button>
	);

export default Row;
