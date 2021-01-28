import React, { useState } from "react";
import { connect } from "react-redux";
import { Property } from "csstype";

import { IBoardItem } from "../../interfaces/components";
import { IMapStateFunc } from "../../interfaces/redux";
import { Button, FileSelect, TextCollapse, Tooltip } from "../common";

import Palette from "./contentBoardTableItemImgViewPalette";

interface IOwnProps {
	item: IBoardItem;
	onChange: (item: IBoardItem) => void;

	filter?: string;
	height: number;
	isDisabled?: boolean;
	className?: string;
	data?: string;
	onDelete?: () => void;
}

interface IStateToProps extends IOwnProps {
	view: Property.ObjectFit;
}

interface IProps extends IStateToProps {}

const View: React.FC<IProps> = props => {
	const { item, view, className = "", height, data, onChange, onDelete } = props;

	const doneIcon = item.isDone ? "-check" : "";

	const [isPaletteVisible, setPaletteVisible] = useState(false);
	const togglePalette = () => setPaletteVisible(!isPaletteVisible);

	const [isBtnsVisible, setBtnsVisible] = useState(false);

	const palette = !isPaletteVisible || !data ? null : <Palette src={data} />;

	return (
		<div
			className="d-flex expander"
			onMouseDown={e => e.stopPropagation()}
			onTouchStart={e => {
				e.stopPropagation();
				setBtnsVisible(!isBtnsVisible);
			}}
		>
			<Tooltip title={item.description ?? ""}>
				<img
					className={className}
					src={data}
					alt="Загрузка..."
					style={{ height, width: "100%", objectFit: view }}
				/>
			</Tooltip>
			<div className="absolute" style={{ width: "100%" }}>
				{palette}
				<div className="hover-only float-right" style={{ visibility: isBtnsVisible ? "visible" : "collapse" }}>
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

const mapState: IMapStateFunc<IStateToProps, IOwnProps> = ({ view }, ownProps) => ({
	...ownProps,
	view: view.item,
});

export default connect(mapState)(View);
