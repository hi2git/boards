import React from "react";
import { observer } from "mobx-react";
import { FormInstance } from "antd/lib/form";

import nameof from "../../utils/nameof";
import store from "../../reducers/settings";
import { Form, FormItem, ValidatedInput } from "../common/forms";
import { Button, confirm } from "../common";

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

	const del = () => confirm({ title: "Подтвердите удаление своей учетной записи", onOk: store.del });

	return (
		<Form ref={ref} item={item} keys={keys} labelCol={{ span: 7 }} onFinish={send}>
			<FormItem label="Удалить свою учетную запись">
				<Button title="Удалить свою учетную запись" danger onClick={del}>
					<i className="fas fa-times" />
				</Button>
			</FormItem>
			<ValidatedInput
				title="Старый пароль"
				keyName={OLD_PASSWORD}
				type="password"
				max={50}
				autoFocus
				isRequired
				onChange={set}
			/>
			<ValidatedInput
				title="Новый пароль"
				autoComplete="new-password"
				keyName={NEW_PASSWORD}
				type="password"
				max={50}
				isRequired
				onChange={set}
			/>
			<ValidatedInput
				title="Подтвердите пароль"
				keyName={CONFIRM_PASSWORD}
				type="password"
				max={50}
				isRequired
				rules={[{ validator: validatePasswordConfirm, message: "Пароли не совпадают" }]}
				onChange={set}
			/>
			<Button type="primary" className="float-right" htmlType="submit" title="OK" disabled={!isAllowSend}>
				OK
			</Button>
		</Form>
	);
};

export default observer(ContentForm);
