import React from "react";
import { observer } from "mobx-react";

import scale from "../../reducers/boardScale";
import posts from "../../reducers/posts";

import Item from "./contentBoardTableItem";
import None from "./contentBoardTableNone";

const DEFAULT_COLS = 3;

interface IProps {
	width?: number;
}

const ContentTable: React.FC<IProps> = ({ width }) => {
	const { items, put, sort, del } = posts;

	const [id, setId] = React.useState("");
	const [touchId, setTouchId] = React.useState("");

	if (items.length < 1) return <None />;

	const replace = async (dragId: string, dropId?: string) => {
		dropId = dropId ?? id;
		const drag = items.find(n => n.id === dragId);
		const over = items.find(n => n.id === dropId);
		if (!drag || !over) return console.error("not found");

		const index = drag.orderNumber;
		drag.orderNumber = over.orderNumber;
		over.orderNumber = index;

		setId("");
		return sort(items);
	};

	const DEF = (width ?? 0) / DEFAULT_COLS;

	const divs = [...items]
		.sort((a, b) => b.orderNumber - a.orderNumber)
		.map((n, i) => {
			const borderW = touchId === n.id ? 10 : 0;
			const left = (i % DEFAULT_COLS) * DEF;
			const top = Math.floor(i / DEFAULT_COLS) * DEF;
			const height = DEF - 5 - borderW;
			return (
				<div
					key={n.id}
					draggable
					style={{
						border: `${borderW}px solid #91d5ff`,
						cursor: "grab",
						width: DEF,
						height: DEF,
						position: "absolute",
						left,
						top,
					}}
					onTouchEnd={async e => {
						e.preventDefault();

						if (!touchId) return setTouchId(n.id);
						if (touchId === n.id) return setTouchId("");

						await replace(touchId, n.id);
						return setTouchId("");
					}}
					onDragEnd={() => replace(n.id)}
					onDragOver={e => {
						e.preventDefault();
						return setId(n.id);
					}}
					onDrop={e => e.preventDefault()}
				>
					<Item item={n} height={height} onChange={put} onDelete={del} />
				</div>
			);
		});

	return (
		<div
			className="border-right border-left w-100"
			style={{ transform: `scale(${scale.value / 100})`, transformOrigin: "top" }}
		>
			{divs}
		</div>
	);
};

export default observer(ContentTable);
