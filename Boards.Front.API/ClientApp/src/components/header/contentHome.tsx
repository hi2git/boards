import React from "react";
import { Link } from "react-router-dom";

import * as urls from "../../constants/urls";
import { MenuItem } from "../common";

interface IProps {}

const Item: React.FC<IProps> = props => {
	return (
		<MenuItem className="float-left mx-0" title="Лента" {...props}>
			<Link to={urls.HOME}>
				Лента <i className="fas fa-th ml-1" />
			</Link>
		</MenuItem>
	);
};

export default Item;
