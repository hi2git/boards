import React from "react";
import { observer } from "mobx-react";

import { IBoardItem } from "../../interfaces/components";
import { Button, FileSelect, Tooltip } from "../common";
import store from "../../reducers/boardControl";

import Palette from "./contentBoardTableItemImgViewPalette";
import DescDlg from "./contentBoardTableItemImgViewDescr";

import view from "../../reducers/view";
import pltStore from "../../reducers/boardPalette";

interface IProps {
	item: IBoardItem;
	onChange: (item: IBoardItem) => void;

	size: number;
	isDisabled?: boolean;
	className?: string;
	data?: string;
	onDelete?: () => void;
}

const View: React.FC<IProps> = props => {
	const { item, className, size, data, onChange, onDelete } = props;

	const doneIcon = item.isDone ? "-check" : "";

	const palette = !pltStore.value || !data ? null : <Palette src={data} />;

	return (
		<div className="d-flex expander">
			<Tooltip title={item.description ?? ""}>
				<img
					className={className}
					src={data}
					alt="Загрузка..."
					style={{ width: size, height: size, objectFit: view.value }}
				/>
			</Tooltip>
			<div className="absolute w-100">
				{palette}
				<div className="hover-only float-right" style={{ visibility: store.value ? "visible" : "collapse" }}>
					<DescDlg item={item} onChange={description => onChange({ ...item, description })} />
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
