import React, { useState } from "react";

import { AlertDanger, Button } from ".";
import { IPost } from "../../interfaces/components";

interface ISelectProps {
	item: IPost;
	onChange: (item: IPost) => void;
	filter?: string;
	title: string;
	isDisabled?: boolean;
	// icon?: string;
	isAdd?: boolean;
}

const Selector: React.FC<ISelectProps> = ({ item, isDisabled, title, onChange, filter = "image/*", isAdd }) => {
	const [error, setError] = useState<string>();

	const changeFile = async (files: FileList) => {
		// const [file] = files;

		[...files].forEach(file => {
			if (!file["type"].includes("image")) {
				return setError("Выбранный файл не является изображением");
			}

			const img = new Image();
			img.onload = async () => {
				setError(undefined);
				const content = img.src.split(",")[1];
				const itm: IPost = { ...item, content };
				onChange(itm);
			};

			const reader = new FileReader();
			reader.onload = () => (img.src = reader.result?.toString() ?? ""); // Запускает img.onload
			reader.onerror = _ => setError("Выбранное изображение недоступно");
			reader.readAsDataURL(file);
		});
	};

	const ref = React.useRef<HTMLInputElement>(null);

	const icon = isAdd ? "plus" : "pencil-alt";

	return (
		<>
			<AlertDanger value={error} />
			<Button title={title} disabled={isDisabled} onClick={() => ref.current?.click()}>
				<i className={`fas fa-${icon}`} />
			</Button>
			<input
				ref={ref}
				type="file"
				multiple
				onChange={e => changeFile(e.target.files as any)}
				accept={filter}
				hidden
			/>
		</>
	);
};

export default Selector;
