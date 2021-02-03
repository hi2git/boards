import React from "react";
import { Button } from "../common";

interface IProps {
	isEdit: boolean;
	isDisabled: boolean;
	onStartEdit: () => void;
	onSubmit: () => void;
	onCancel: () => void;
}

const EditBtn: React.FC<IProps> = ({ isEdit, isDisabled, onStartEdit, onCancel, onSubmit }) => {
	return isEdit ? (
		<>
			<Button title="Сохранить" type="link" disabled={isDisabled} onClick={onSubmit}>
				<i className="fas fa-check" />
			</Button>
			<Button title="Отмена" type="link" className="pl-0" onClick={onCancel}>
				<i className="fas fa-ban" />
			</Button>
		</>
	) : (
		<Button title="Изменить" type="link" onClick={onStartEdit}>
			<i className="fas fa-pen" />
		</Button>
	);
};

export default EditBtn;
