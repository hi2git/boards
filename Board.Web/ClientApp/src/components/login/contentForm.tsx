import React, { useState } from "react";
import { FormInstance } from "antd/lib/form";
import { Link } from "react-router-dom";

import { Form, ValidatedInput, isValidateError, FormItem } from "../common/forms";
import nameof from "../../utils/nameof";
import * as urls from "../../constants/urls";
import { IUserLogin } from "../../interfaces/components";
import { Button } from "../common";

const LOGIN = nameof<IUserLogin>("login");
const PASSWORD = nameof<IUserLogin>("password");

interface IProps {
	onOk: (item: IUserLogin) => void;
}

const ContentForm: React.FC<IProps> = ({ onOk }) => {
	const [login, setLogin] = useState<string>("");
	const [password, setPassword] = useState<string>("");

	const item: IUserLogin = { login, password };

	const submit = async () => {
		const isError = await isValidateError(ref);
		if (!isError) onOk(item);
	};

	let ref: FormInstance | null = null;
	return (
		<Form
			ref={n => (ref = n)}
			autoComplete="on"
			keys={[LOGIN, PASSWORD]}
			item={{ login, password }}
			labelCol={{ span: 6 }}
			onFinish={submit}
		>
			<ValidatedInput keyName={LOGIN} title="Логин" onChange={(_, n) => setLogin(n)} isRequired />
			<ValidatedInput
				keyName={PASSWORD}
				title="Пароль"
				type="password"
				onChange={(_, n) => setPassword(n)}
				isRequired
			/>
			<FormItem>
				<Button type="primary" htmlType="submit" className="float-right ml-2" title="Войти">
					Войти
				</Button>
				<Button type="link" className="float-right" title="Регистрация">
					<Link className="float-right" to={urls.SIGNUP}>
						Регистрация
					</Link>
				</Button>
			</FormItem>
		</Form>
	);
};

export default ContentForm;
