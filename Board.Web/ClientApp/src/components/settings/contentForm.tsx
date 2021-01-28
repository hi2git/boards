import React from "react";
import { observer } from "mobx-react";
import { FormInstance } from "antd/lib/form";

import nameof from "../../utils/nameof";
import store from "../../reducers/settings";
import { Form, ValidatedInput } from "../common/forms";
import { Button } from "../common";

const OLD_PASSWORD = nameof<typeof store.item>("oldPassword");
const NEW_PASSWORD = nameof<typeof store.item>("newPassword");
const CONFIRM_PASSWORD = nameof<typeof store.item>("confirmPassword");
const keys = [OLD_PASSWORD, NEW_PASSWORD, CONFIRM_PASSWORD];

interface IProps {}

const ContentForm: React.FC<IProps> = () => {
	const ref = React.useRef<FormInstance>(null);

	const { item, isPasswordChanged, isPasswordError, isAllowSend, set, send } = store;

	const validatePasswordConfirm = (rule: any, _: string, callback: (msg?: string) => void) => {
		const msg = isPasswordChanged && isPasswordError ? rule.message : undefined;
		callback(msg);
    };
    
	return (
		<Form ref={ref} item={item} keys={keys} labelCol={{ span: 7 }}>
			<ValidatedInput title="Старый пароль" keyName={OLD_PASSWORD} type="password" isRequired onChange={set} />
			<ValidatedInput title="Новый пароль" keyName={NEW_PASSWORD} type="password" isRequired onChange={set} />
			<ValidatedInput
				title="Подтвердите пароль"
				keyName={CONFIRM_PASSWORD}
				type="password"
				isRequired
				rules={[{ validator: validatePasswordConfirm, message: "Пароли не совпадают" }]}
				onChange={set}
			/>
			<Button type="primary" className="float-right" title="OK" disabled={!isAllowSend} onClick={send}>
				OK
			</Button>
		</Form>
	);
};

export default observer(ContentForm);
