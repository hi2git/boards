import React from "react";
import { Rule } from "antd/lib/form";

import { FormItem, requireRule } from ".";
import { Input } from "..";
import { IInputProps } from "../input";

const emailRule: Rule = { type: "email", message: "Неверный формат email" };
const requiredRule: Rule = { whitespace: true, message: "Обязательное поле" };

interface IProps extends Omit<IInputProps, "onChange"> {
	keyName: string;
	title: string;
	className?: string;
	onChange: (key: string, value: string) => void;
	type?: "password" | "text" | "email";
	rules?: Array<Rule>;
	max?: number;
	isRequired?: boolean;
	isReadOnly?: boolean;
	isMaxChecked?: boolean;
	// isEmail?: boolean;
	isInline?: boolean;
}

export const ValidatedInput: React.FC<IProps> = props => {
	const {
		isInline,
		title,
		keyName,
		className,
		type = "text",
		max = 255,
		onChange,
		isRequired,
		isReadOnly,
		rules = [requiredRule],
		...others
	} = props;

	const maxRule = { max, message: `${title} превышает ${max} символов` };

	let allRules = isRequired ? [requireRule, ...rules] : rules;
	allRules = props.isMaxChecked ? [maxRule, ...allRules] : allRules;
	allRules = type === "email" ? [emailRule, ...allRules] : allRules;

	return (
		<FormItem label={isInline ? null : title} name={keyName} className={className} rules={allRules}>
			<Input
				{...others}
				disabled={isReadOnly}
				placeholder={title}
				name={keyName}
				type={type}
				maxLength={max}
				allowClear={!isRequired}
				onChange={e => onChange(keyName, e.target.value)}
			/>
		</FormItem>
	);
};

export default ValidatedInput;
