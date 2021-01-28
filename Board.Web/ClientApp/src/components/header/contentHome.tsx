import React from "react";

import { MenuItem } from "../common";
import { Link } from "react-router-dom";

interface IProps {}

const Item: React.FC<IProps> = props => {
	return (
		<MenuItem className="ml-0" title="Лента" {...props}>
			<Link to="/">
				Лента <i className="fas fa-th ml-1" />
			</Link>
		</MenuItem>
	);
};

export default Item;
