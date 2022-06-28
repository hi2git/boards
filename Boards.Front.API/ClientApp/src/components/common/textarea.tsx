import React from "react";
import { Input } from "antd";
import { TextAreaProps } from "antd/lib/input";

const Area = Input.TextArea;

interface IProps extends Omit<TextAreaProps, "onChange"> {
	onChange: (value: string) => void;
}

const TextArea: React.FC<IProps> = ({ onChange, ...props }) => (
	<Area autoSize {...props} onMouseDown={e => e.stopPropagation()} onChange={e => onChange(e.target.value)} />
);

export default TextArea;
