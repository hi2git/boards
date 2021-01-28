import React from "react";
import { Link } from "react-router-dom";

import * as urls from "../../constants/urls";
import { MenuItem } from "../common";

interface IProps {}

const Item: React.FC<IProps> = props => {
	return (
		<MenuItem title="Контакты" className="float-right" {...props}>
			<Link to={urls.CONTACTS}>
				Контакты <i className="far fa-question-circle ml-1" />
			</Link>
		</MenuItem>
	);
};

export default Item;
