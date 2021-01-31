import React, { useState } from "react";
import { observer } from "mobx-react";

import { IBoardItem } from "../../interfaces/components";
import { Button, FileSelect, TextCollapse, Tooltip } from "../common";
import store from "../../reducers/boardControl";

import Palette from "./contentBoardTableItemImgViewPalette";

import view from "../../reducers/view";

interface IProps {
	item: IBoardItem;
	onChange: (item: IBoardItem) => void;

	filter?: string;
	height: number;
	isDisabled?: boolean;
	className?: string;
	data?: string;
	onDelete?: () => void;
}

const View: React.FC<IProps> = props => {
	const { item, className = "", height, data, onChange, onDelete } = props;

	const doneIcon = item.isDone ? "-check" : "";

	const [isPaletteVisible, setPaletteVisible] = useState(false);
	const togglePalette = () => setPaletteVisible(!isPaletteVisible);

	const palette = !isPaletteVisible || !data ? null : <Palette src={data} />;

	return (
		<div className="d-flex expander">
			<Tooltip title={item.description ?? ""}>
				<img
					className={className}
					src={data}
					alt="Загрузка..."
					style={{ height, width: "100%", objectFit: view.value }}
				/>
			</Tooltip>
			<div className="absolute" style={{ width: "100%" }}>
				{palette}
				<div
					className="hover-only float-right"
					style={{ visibility: store.value ? "visible" : "collapse" }}
				>
					<Button title="Палитра" onClick={togglePalette}>
						<i className="fas fa-palette" />
					</Button>
					<TextCollapse
						value={item.description}
						onChange={description => onChange({ ...item, description })}
					/>
					<Button title="Отметить отправленным" onClick={() => onChange({ ...item, isDone: !item.isDone })}>
						<i className={`far fa${doneIcon}-square`} />
					</Button>
					<FileSelect title="Изменить" {...props} />
					<Button title="Удалить" onClick={() => onDelete?.()}>
						<i className="fas fa-times" />
					</Button>
				</div>
			</div>
		</div>
	);
};

export default observer(View);
