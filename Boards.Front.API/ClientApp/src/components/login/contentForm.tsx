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
			labelCol={{ span: 4 }}
			onFinish={submit}
		>
			<ValidatedInput
				keyName={LOGIN}
				title="Логин"
				max={50}
				onChange={(_, n) => setLogin(n)}
				isRequired
				autoFocus
			/>
			<ValidatedInput
				keyName={PASSWORD}
				title="Пароль"
				type="password"
				max={50}
				onChange={(_, n) => setPassword(n)}
				isRequired
			/>
			<FormItem wrapperCol={{ offset: 4 }}>
				<Button type="primary" htmlType="submit" title="Войти">
					Войти
				</Button>
				<Button type="link" title="Регистрация">
					<Link to={urls.SIGNUP}>Регистрация</Link>
				</Button>
			</FormItem>
		</Form>
	);
};

export default ContentForm;
