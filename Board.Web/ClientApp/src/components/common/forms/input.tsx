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
	type?: "password" | "text";
	rules?: Array<Rule>;
	max?: number;
	isRequired?: boolean;
	isReadOnly?: boolean;
	isMaxChecked?: boolean;
	isEmail?: boolean;
	isAutoFocused?: boolean;
	isInline?: boolean;
}

export const ValidatedInput: React.FC<IProps> = props => {
	const {
		isInline,
		isAutoFocused,
		title,
		keyName,
		className,
		type = "text",
		max = 255,
		onChange,
		isRequired,
		isReadOnly,
		rules = [requiredRule],
	} = props;

	const maxRule = { max: max, message: `${title} превышает ${max} символов` };

	let allRules = isRequired ? [requireRule, ...rules] : rules;
	allRules = props.isMaxChecked ? [maxRule, ...allRules] : allRules;
	allRules = props.isEmail ? [emailRule, ...allRules] : allRules;

	return (
		<FormItem label={isInline ? null : title} name={keyName} className={className} rules={allRules}>
			<Input
				disabled={isReadOnly}
				placeholder={title}
				name={keyName}
				type={type}
				maxLength={max}
				autoFocus={isAutoFocused}
				onChange={e => onChange(keyName, e.target.value)}
			/>
		</FormItem>
	);
};

export default ValidatedInput;
