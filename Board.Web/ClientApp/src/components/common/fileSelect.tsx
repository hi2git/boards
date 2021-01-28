import React, { useState } from "react";

import { AlertDanger, Button } from ".";
import { IBoardItem } from "../../interfaces/components";

interface ISelectProps {
	item: IBoardItem;
	onChange: (item: IBoardItem) => void;
	filter?: string;
	title: string;
	isDisabled?: boolean;
	// icon?: string;
	isAdd?: boolean;
}

const Selector: React.FC<ISelectProps> = ({ item, isDisabled, title, onChange, filter = "*.*", isAdd }) => {
	const [error, setError] = useState<string>();

	const changeFile = async (files: FileList) => {
		const [file] = files;

		if (!file["type"].includes("image")) {
			return setError("Выбранный файл не является изображением");
		}

		const img = new Image();
		img.onload = async () => {
			setError(undefined);
			const content = img.src.split(",")[1];
			const itm: IBoardItem = { ...item, content };
			onChange(itm);
		};

		const reader = new FileReader();
		reader.onload = () => (img.src = reader.result?.toString() ?? ""); // Запускает img.onload
		reader.onerror = _ => setError("Выбранное изображение недоступно");
		reader.readAsDataURL(file);
	};

	let ref: HTMLInputElement | null = null;

	const icon = isAdd ? "plus" : "pencil-alt";
	// const title = isAdd ? "Добавить" : "Изменить";

	return (
		<>
			<AlertDanger value={error} />
			<Button title={title} disabled={isDisabled} onClick={() => ref?.click()}>
				<i className={`fas fa-${icon}`} />
			</Button>
			<input
				ref={n => (ref = n)}
				type="file"
				onChange={e => changeFile(e.target.files as any)}
				accept={filter}
				hidden
			/>
		</>
	);
};

export default Selector;
