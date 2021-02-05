import React from "react";
import { observer } from "mobx-react";

import { IIdName } from "../../interfaces/components";
import { Button, confirm } from "../common";
import { Form, ValidatedInput } from "../common/forms";
import nameof from "../../utils/nameof";
import boards from "../../reducers/boards";
import board from "../../reducers/board";

import EditSaveBtn from "./contentTableRowEdit";

interface IProps {
	item: IIdName;
}

const Row: React.FC<IProps> = ({ item }) => {
	const [isEdit, setEdit] = React.useState(false); // store.put(item)
	const [name, setName] = React.useState(item.name);

	let newItem = { ...item, name };
	const submit = async () => {
		setEdit(false);
		await boards.put(newItem);
	};

	const cancel = async () => {
		setEdit(false);
		await setName(item.name);
	};

	const del = async () => confirm({ title: "Подтвердите удаление", onOk: () => boards.del(newItem.id) });

	const borderCls = board.value?.id === item.id ? "border-left border-right" : undefined;

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

const NAME = nameof<IIdName>("name");

interface IValueCellProps {
	isEdit: boolean;
	item: IIdName;
	onChange: (value: string) => void;
	onSubmit: () => void;
}

const ValueCell: React.FC<IValueCellProps> = ({ isEdit, item, onChange, onSubmit }) => {
	return isEdit ? (
		<Form keys={[NAME]} item={item} layout="inline" onFinish={onSubmit}>
			<ValidatedInput
				title="Название"
				keyName={NAME}
				// max={50}
				isInline
				isRequired
				value={item.name}
				onChange={(_, value) => onChange(value)}
			/>
		</Form>
	) : (
		<Button title="Показать" type="link" className="px-0" onClick={() => board.setValue(item)}>
			{item.name}
		</Button>
	);
};

export default observer(Row);
