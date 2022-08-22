import React from "react";
import { Footer } from "../common/layouts";

interface IProps {}

const Content: React.FC<IProps> = () => {
	const year = new Date().getFullYear();
	return <Footer>Boards - Your way of planning, {year} </Footer>;
};

export default Content;
