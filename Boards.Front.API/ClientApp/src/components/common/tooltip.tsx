import React from "react";
import { Tooltip as Tltp } from "antd";
// import { TooltipPropsWithOverlay } from "antd/lib/tooltip";

// interface IProps extends TooltipPropsWithOverlay {}
interface IProps {
	title: string;
}

const Tooltip: React.FC<IProps> = props => <Tltp {...props} />;

export default Tooltip;
