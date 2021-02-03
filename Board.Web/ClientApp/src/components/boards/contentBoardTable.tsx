import React, { useState } from "react";
import { observer } from "mobx-react";
import { Link } from "react-router-dom";

import { Layout, WidthProvider, Responsive } from "react-grid-layout";
import { IBoardItem } from "../../interfaces/components";
import scale from "../../reducers/boardScale";
import boardItems from "../../reducers/boardItems";

import Item from "./contentBoardTableItem";
import { FileSelect } from "../common";
import { addItem } from "./contentBoard";

const GridLayout = WidthProvider(Responsive);

const MAX_COLS = 3;
const DEFAULT_COLS = 3;
const WIDTH_COL = DEFAULT_COLS / MAX_COLS;

interface IProps {}

const ContentTable: React.FC<IProps> = () => {
	const { items, put, sort, del } = boardItems;

	const [height, setHeight] = useState(0);

	if (items.length < 1)
		return (
			<p>
				Для добавления новых постов нажмите{" "}
				<FileSelect title="Добавить пост" item={addItem} onChange={boardItems.post} isAdd />
			</p>
		);

	const getLayout: (items: Array<IBoardItem>) => Layout[] = items => {
		const results: Array<Layout> = [...items]
			.sort((a, b) => a.orderNumber - b.orderNumber)
			.map((n, i) => ({
				i: n.id,
				x: (i % MAX_COLS) * WIDTH_COL,
				y: Math.floor(i / MAX_COLS),
				w: WIDTH_COL,
				h: 1,
				isResizable: false,
			}));

		return results;
	};

	const layout = getLayout(items);

	const change = (layout: Layout[]) => {
		const outItems = layout.map(n => {
			const orderNumber = n.y * MAX_COLS + n.x / WIDTH_COL;
			const item = items.find(m => n.i === m.id) as IBoardItem;
			return { ...item, orderNumber };
		});
		return sort(outItems);
	};

	const replace = (_: Layout[], old: Layout, nw: Layout) => {
		const wasIndex = layout.findIndex(n => n.x === nw.x && n.y === nw.y);
		layout[wasIndex].x = old.x;
		layout[wasIndex].y = old.y;

		const nwIndex = layout.findIndex(n => n.i === nw.i);
		layout[nwIndex].x = nw.x;
		layout[nwIndex].y = nw.y;
		return change(layout);
	};

	const divs = items.map(n => (
		<div key={n.id} style={{ width: height }}>
			<Item item={n} height={height} onChange={put} onDelete={del} />
		</div>
	));

	return (
		<GridLayout
			style={{ transform: `scale(${scale.value / 100})`, transformOrigin: "top" }}
			className="border-right border-left"
			layouts={{ lg: layout, md: layout, sm: layout, xs: layout, xxs: layout }}
			cols={{
				lg: DEFAULT_COLS,
				md: DEFAULT_COLS,
				sm: DEFAULT_COLS,
				xs: DEFAULT_COLS,
				xxs: DEFAULT_COLS,
			}}
			rowHeight={height}
			margin={[3, 3]}
			onDragStop={replace}
			onWidthChange={n => setHeight(n / 3 - 6)}
		>
			{divs}
		</GridLayout>
	);
};

export default observer(ContentTable);
