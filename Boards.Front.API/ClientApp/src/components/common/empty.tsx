import React from "react";
import { Empty as Wrapped } from "antd";
import { EmptyProps } from "antd/lib/empty";

interface IProps extends EmptyProps {}

const Empty: React.FC<IProps> = props => {
	return <Wrapped {...props} />;
};

export default Empty;
