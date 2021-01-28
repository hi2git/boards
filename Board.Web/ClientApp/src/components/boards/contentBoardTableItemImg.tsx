import React from "react";
import { IBoardItem } from "../../interfaces/components";

// import { Button, FileSelect } from "../common";
import View from "./contentBoardTableItemImgView";

interface IProps {
	item: IBoardItem;
	height: number;
	onChange: (item: IBoardItem) => void;
	onDelete?: (id: string) => void;
}

interface IState {
	data?: string;
	error: string | null;
}

class Img extends React.Component<IProps, IState> {
	state: IState = {
		error: null,
	};

	componentDidMount = () => {
		const { item } = this.props;
		// const data = item.content ? `data:image/png;base64, ${item.content}` : undefined;
		const data = item.content ? `data:image/png;base64, ${item.content}` : `/api/image?id=${item.id}`;
		return this.setState({ data, error: null });
	};

	componentDidUpdate = (prevProps: IProps) => {
		const { item } = this.props;

		if (prevProps.item.content === item.content) return;

		// const data = item.content ? `data:image/png;base64,${item.content}` : undefined;
		const data = item.content ? `data:image/png;base64, ${item.content}` : `/api/image?id=${item.id}`;
		return this.setState({ data, error: null });
	};

	delete = () => {
		const { item, onDelete } = this.props;
		return onDelete?.(item.id);
	};

	render = () => {
		const { item, height, onChange } = this.props;
		const { data, error } = this.state;

		return (
			<>
				{error}
				<View
					item={item}
					filter=".png,.jpg,.gif,.ico"
					onChange={onChange}
					onDelete={this.delete}
					data={data}
					height={height}
				/>
			</>
		);
	};
}

export default Img;
