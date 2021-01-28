import React from "react";
import { Rule } from "antd/lib/form";

import { FormItem, requireRule } from ".";
import { Input } from "..";

const emailRule: Rule = { type: "email", message: "Неверный формат email" };

interface IProps {
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
}

export const ValidatedInput: React.FC<IProps> = props => {
	const { isAutoFocused, title, keyName, className, type = "text", max = 255, onChange } = props;
	const { isRequired, isReadOnly, rules = [{ whitespace: true, message: "Обязательное поле" }] } = props;

	const maxRule = { max: max, message: `${title} превышает ${max} символов` };

	let allRules = isRequired ? [requireRule, ...rules] : rules;
	allRules = props.isMaxChecked ? [maxRule, ...allRules] : allRules;
	allRules = props.isEmail ? [emailRule, ...allRules] : allRules;

	return (
		<FormItem label={title} name={keyName} className={className} rules={allRules}>
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
