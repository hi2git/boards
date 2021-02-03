import { observer } from "mobx-react";
import React from "react";
import { Link } from "react-router-dom";

import * as urls from "../../constants/urls";
import { MenuItem } from "../common";

import board from "../../reducers/board";

import "./contentTitle.css";

interface IProps {}

const Item: React.FC<IProps> = props => {
	return (
		<MenuItem title={board.value?.name} {...props}>
			<Link to={urls.HOME}>
				<span className="header-title">{board.value?.name}</span>
			</Link>
		</MenuItem>
	);
};

export default observer(Item);
