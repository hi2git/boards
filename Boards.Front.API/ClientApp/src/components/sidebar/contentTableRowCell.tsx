import React from "react";

import { IIdName } from "../../interfaces/components";
import board from "../../reducers/board";
import nameof from "../../utils/nameof";
import { Button } from "../common";
import { Form, ValidatedInput } from "../common/forms";

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
				max={50}
				autoFocus
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

export default ValueCell;
