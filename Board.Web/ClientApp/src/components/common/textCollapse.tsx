import React, { useState } from "react";
import { Button, Modal, TextArea } from ".";

interface IProps {
	value?: string;
	onChange: (value?: string) => void;
}

const TextCollapse: React.FC<IProps> = ({ value, onChange }) => {
	const [isVisible, setIsVisible] = useState(false);
	const [val, setVal] = useState(value);

	const input = !isVisible ? null : <TextArea value={val} onChange={setVal} />;

	return (
		<>
			<Button title="Описание" onClick={() => setIsVisible(!isVisible)}>
				<i className="fas fa-hashtag" />
			</Button>
			<Modal
				title="Описание"
				visible={isVisible}
				onOk={() => {
					setIsVisible(false);
					onChange(val);
				}}
				onCancel={() => setIsVisible(false)}
			>
				{input}
			</Modal>
		</>
	);
};

export default TextCollapse;
