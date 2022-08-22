import React, { useState } from "react";

import { AlertDanger, Button } from ".";
import { IPost } from "../../interfaces/components";

interface IProps {
	item: IPost;
	onChange: (item: IPost) => void;
	className?: string;
	filter?: string;
	title: string;
	isDisabled?: boolean;
	// icon?: string;
	isAdd?: boolean;
}

const Selector: React.FC<IProps> = ({ item, isDisabled, className, title, onChange, filter = "image/*", isAdd }) => {
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
			<Button title={title} className={className} disabled={isDisabled} onClick={() => ref.current?.click()}>
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
