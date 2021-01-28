// import React from "react";

// interface IProps {
// 	value?: string;
// 	onChange: (value: string) => void;
// }

// const Input: React.FC<IProps> = ({ value, onChange }) => (
// 	<input
// 		type="text"
// 		className="form-control"
// 		style={{ width: "50%" }}
// 		value={value}
// 		onMouseDown={e => e.stopPropagation()}
// 		onChange={e => onChange(e.target.value)}
// 	/>
// );

// export default Input;

import React from "react";
import { Input as Inpt } from "antd";
import { InputProps } from "antd/lib/input";

// export const defaultAutocomplete = "new-password"; // autoComplete={defaultAutocomplete}

interface IProps extends InputProps {}

const Input: React.FC<IProps> = props => <Inpt {...props} />;

export default Input;
