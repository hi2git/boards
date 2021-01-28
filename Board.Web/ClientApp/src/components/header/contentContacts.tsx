import React from "react";

import { MenuItem } from "../common";
import { Link } from "react-router-dom";

interface IProps {}

const Item: React.FC<IProps> = props => {
	return (
		<MenuItem title="Контакты" className="float-right" {...props}>
			<Link to="/contacts">
				Контакты <i className="far fa-question-circle ml-1" />
			</Link>
		</MenuItem>
	);
};

export default Item;
