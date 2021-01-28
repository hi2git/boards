import React from "react";
import { usePalette } from "color-thief-react";
import { LoadablePanel } from "../common";

import "./contentBoardTableItemImgViewPalette.css";

interface IProps {
	src: string;
}

const Palette: React.FC<IProps> = ({ src }) => {
	const { data, loading } = usePalette(src, 6, "hex");
	const cells = (data as string[])?.map((n, i) => (
		<td key={`color-${i}`} className="px-1">
			<div className="rounded" style={{ backgroundColor: n }}></div>
		</td>
	));
	return (
		<LoadablePanel isLoading={loading}>
			<table className="table mb-1">
				<tbody>
					<tr className="border-bottom">{cells}</tr>
				</tbody>
			</table>
		</LoadablePanel>
	);
};

export default Palette;
