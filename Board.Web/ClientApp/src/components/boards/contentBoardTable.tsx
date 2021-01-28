import React, { useState } from "react";
import { Layout, WidthProvider, Responsive } from "react-grid-layout";
import { IBoardItem } from "../../interfaces/components";

import Item from "./contentBoardTableItem";

const GridLayout = WidthProvider(Responsive);

const MAX_COLS = 3;
const DEFAULT_COLS = 3;
const WIDTH_COL = DEFAULT_COLS / MAX_COLS;

interface IProps {
	items: Array<IBoardItem>;
	onChange: (item: IBoardItem) => void;
	onSorted: (items: Array<IBoardItem>) => void;
	onDelete: (id: string) => void;
}

const ContentTable: React.FC<IProps> = ({ items, onChange, onSorted, onDelete }) => {
	const getLayout: (items: Array<IBoardItem>) => Layout[] = items => {
		const results: Array<Layout> = items
			.sort((a, b) => a.orderNumber - b.orderNumber)
			.map((n, i) => ({
				i: n.id,
				x: (i % MAX_COLS) * WIDTH_COL,
				y: Math.floor(i / MAX_COLS),
				w: WIDTH_COL,
				h: 1,
				minW: WIDTH_COL,
				maxW: WIDTH_COL,
				// static: n.id === "add",
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
		return onSorted(outItems);
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

	const [height, setHeight] = useState(0);

	const divs = items.map(n => (
		<div key={n.id}>
			<Item item={n} height={height} onChange={onChange} onDelete={onDelete} />
		</div>
	));

	return (
		<div className="row">
			<div className="col-12">
				<GridLayout
					className="layout border-right border-left"
					layouts={{ lg: layout, md: layout, sm: layout, xs: layout, xxs: layout }}
					cols={{ lg: DEFAULT_COLS, md: DEFAULT_COLS, sm: DEFAULT_COLS, xs: DEFAULT_COLS, xxs: DEFAULT_COLS }}
					rowHeight={height}
					margin={[5, 5]}
					onDragStop={replace}
					onWidthChange={n => setHeight(n / 3)}
				>
					{divs}
				</GridLayout>
			</div>
		</div>
	);
};

export default ContentTable;
