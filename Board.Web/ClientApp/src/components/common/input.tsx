import React from "react";
import { Input as Inpt } from "antd";
import { InputProps } from "antd/lib/input";

// export const defaultAutocomplete = "new-password"; // autoComplete={defaultAutocomplete}

export interface IInputProps extends InputProps {}

const Input: React.FC<IInputProps> = props => <Inpt {...props} />;

export default Input;
