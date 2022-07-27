import React from "react";
import { Input as Inpt } from "antd";
import { InputProps } from "antd/lib/input";

// export const defaultAutocomplete = "new-password"; // autoComplete={defaultAutocomplete}

export interface IInputProps extends InputProps {}

const Input: React.FC<IInputProps> = props =>
	props.type === "password" ? (
		<Inpt.Password autoComplete="new-password" {...props} />
	) : (
		<Inpt autoComplete="new-password" {...props} />
	);

export default Input;
