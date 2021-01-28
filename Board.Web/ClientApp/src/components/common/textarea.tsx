import React from "react";
import { Input } from "antd";

const Area = Input.TextArea;

interface IProps {
	value?: string;
	onChange: (value: string) => void;
}

const TextArea: React.FC<IProps> = ({ value, onChange }) => {
	return (
		<Area autoSize value={value} onMouseDown={e => e.stopPropagation()} onChange={e => onChange(e.target.value)} />
	);
};

export default TextArea;
