import React from "react";
import { Link } from "react-router-dom";
import { observer } from "mobx-react";
import { FormInstance } from "antd/lib/form";

import { Captcha } from "../common";
import store from "../../reducers/signup";
import nameof from "../../utils/nameof";
import * as urls from "../../constants/urls";
import Button from "../common/button";
import { Form, FormItem, ValidatedInput } from "../common/forms";
import { IUserLogin } from "../../interfaces/components";

const NAME = nameof<IUserLogin>("login");
const PASSWORD = nameof<IUserLogin>("password");
const EMAIL = nameof<IUserLogin>("email");
const CAPTCHA = nameof<IUserLogin>("captcha");
const keys = [NAME, PASSWORD, EMAIL];

interface IProps {}

const Content: React.FC<IProps> = () => {
	const ref = React.useRef<FormInstance>(null);

	const { item, isAllowSend, set, send } = store;

	return (
		<Form ref={ref} item={item} keys={keys} labelCol={{ span: 4 }} onFinish={send}>
			<ValidatedInput title="Логин" keyName={NAME} max={50} isRequired autoFocus onChange={set} />
			<ValidatedInput title="Пароль" keyName={PASSWORD} max={50} isRequired type="password" onChange={set} />
			<ValidatedInput title="Email" keyName={EMAIL} max={50} isRequired type="email" onChange={set} />

			<FormItem
				label="Проверка"
				name={CAPTCHA}
				rules={[{ required: true, message: "Необходимо пройти проверку" }]}
			>
				<Captcha onChange={token => set(CAPTCHA, token ?? undefined)} />
			</FormItem>
			<FormItem wrapperCol={{ offset: 4 }}>
				<Button type="primary" title="Зарегистрироваться" disabled={!isAllowSend} htmlType="submit">
					Зарегистрироваться
				</Button>
				<Button type="link" title="Назад">
					<Link to={urls.LOGIN}>Назад</Link>
				</Button>
			</FormItem>
		</Form>
	);
};

export default observer(Content);
