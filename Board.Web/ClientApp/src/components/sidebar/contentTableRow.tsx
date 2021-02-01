import React from "react";
import { Link } from "react-router-dom";

import { IIdName } from "../../interfaces/components";
import { Button, Input } from "../common";
import store from "../../reducers/boards";

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
		store.put(newItem);
	};

	return (
		<tr>
			<td>
				<ValueCell isEdit={isEdit} value={newItem.name} onChange={setName} />
			</td>
			<td className="text-right">
				<EditSaveBtn isEdit={isEdit} onStartEdit={() => setEdit(true)} onSubmit={submit} />
				<Button title="Удалить" type="link" onClick={() => store.del(newItem.id)}>
					<i className="fas fa-times" />
				</Button>
			</td>
		</tr>
	);
};

interface IValueCellProps {
	isEdit: boolean;
	value: string;
	onChange: (value: string) => void;
}

const ValueCell: React.FC<IValueCellProps> = ({ isEdit, value, onChange }) => {
	return isEdit ? (
		<Input value={value} onChange={e => onChange(e.target.value)} />
	) : (
		<Link to={`/${value}`}>{value}</Link>
	);
};

export default Row;
