import React from "react";
import { Button } from "../common";

interface IProps {
	isEdit: boolean;
	onStartEdit: () => void;
	onSubmit: () => void;
}

const EditBtn: React.FC<IProps> = ({ isEdit, onStartEdit, onSubmit }) => {
	return isEdit ? (
		<Button title="Сохранить" onClick={onSubmit}>
			<i className="fas fa-check" />
		</Button>
	) : (
		<Button title="Изменить" type="link" onClick={onStartEdit}>
			<i className="fas fa-pen" />
		</Button>
	);
};

export default EditBtn;
