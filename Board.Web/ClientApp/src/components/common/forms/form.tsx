import React from "react";
import moment from "moment";
import { Form as Frm } from "antd";
import { FormProps, FormInstance } from "antd/lib/form";

const SERVER_DATE_FORMAT: string = "YYYY-MM-DD H:mm:ss";

interface IFunc {
	(form: FormInstance | null): Promise<boolean>;
}

export const useForm = () => Frm.useForm()[0];

export const isValidateError: IFunc = async form => {
	if (!form) return true;
	try {
		await form.validateFields();
	} catch (e) {
		return e.errorFields?.length > 0;
	}
	return false;
};

interface Store {
	[name: string]: any;
}

interface IProps extends FormProps {
	keys: Array<string>;
	item: object | null | undefined;
	dateKeys?: Array<string>;
}

interface IInnerProps {
	innerRef: React.MutableRefObject<FormInstance> | null;
}

interface IAllProps extends IProps, IInnerProps {}

class FormClass extends React.Component<IAllProps> {
	componentDidUpdate = (prev: IAllProps) => {
		if (prev.item === this.props.item) return;
		this.props.innerRef?.current?.setFieldsValue(this.values);
	};

	get values() {
		const { keys, item, dateKeys } = this.props;
		let initialValues: Store = {};
		keys?.forEach(n => {
			const itemValue = item?.[n as keyof object] ?? null;

			const dateValue = dateKeys?.includes(n) ? (itemValue ? moment(itemValue, SERVER_DATE_FORMAT) : null) : null; // DO not use && without null. Try count: 1
			const value = dateValue ?? itemValue;
			initialValues = { ...initialValues, [n]: value };
		});
		return initialValues;
	}

	render = () => {
		const { innerRef, keys, item, labelCol, children, dateKeys, layout = "horizontal", ...otherProps } = this.props;
		const lblCol = labelCol ?? (layout === "horizontal" ? { span: 3 } : undefined);
		const initialValues = this.values;

		return (
			<Frm
				ref={innerRef}
				initialValues={initialValues}
				colon={false}
				autoComplete="off"
				labelAlign="left"
				layout={layout}
				labelCol={lblCol}
				{...otherProps}
			>
				{children}
			</Frm>
		);
	};
}

export default React.forwardRef<FormInstance, IProps>((props, ref) => (
	<FormClass {...props} innerRef={ref as React.MutableRefObject<FormInstance>} />
));
