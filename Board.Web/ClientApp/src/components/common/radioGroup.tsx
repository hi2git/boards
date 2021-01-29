import React from "react";
import { Radio } from "antd";
import { RadioGroupProps } from "antd/lib/radio";

import { Tooltip } from ".";

interface Titleable {
	id: string;
	title: string;
	label: React.ReactNode;
}

interface IProps extends RadioGroupProps {
	opts: Titleable[];
}

const RadioGroup: React.FC<IProps> = ({ opts, ...props }) => {
	const options = opts?.map(n => ({ label: <Tooltip title={n.title}>{n.label}</Tooltip>, value: n.id }));
	return <Radio.Group options={options} {...props} optionType="button" />; //  buttonStyle="solid"
};

export default RadioGroup;
