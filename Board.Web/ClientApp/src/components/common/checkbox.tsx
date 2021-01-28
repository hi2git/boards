import React from "react";

interface IProps {
	value: boolean;
	onChange: (value: boolean) => void;
}

const Checkbox: React.FC<IProps> = ({ value, onChange }) => (
	<input
		className="mr-2 p-2 btn btn-secondary"
		type="checkbox"
		checked={value}
		onClick={e => console.log("click", e)}
		onMouseDown={e => {
			console.log("down", e);
			// e.preventDefault();
		}}
		onChange={e => {
			console.log(e);
			e.preventDefault();
			onChange(e.target.checked);
		}}
	/>
);

export default Checkbox;
