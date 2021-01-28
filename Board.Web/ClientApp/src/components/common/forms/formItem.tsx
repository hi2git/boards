import React from "react";
import { Form } from "antd";
import { FormItemProps, Rule } from "antd/lib/form";

export const requireRule: Rule = { required: true, message: "Обязательное поле" };

interface IProps extends FormItemProps {}

const FormItem: React.FC<IProps> = ({ className = "", children, ...props }) => (
	<Form.Item {...props} className={`my-2 ${className}`}>
		{children}
	</Form.Item>
);

export default FormItem;
