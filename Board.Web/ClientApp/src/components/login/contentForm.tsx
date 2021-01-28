import React, { useState } from "react";
import { Form, ValidatedInput, isValidateError } from "../common/forms";
import nameof from "../../utils/nameof";
import { IUserLogin } from "../../interfaces/components";
import { FormInstance } from "antd/lib/form";
import { Button } from "antd";

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
		<Form ref={n => (ref = n)} keys={[LOGIN, PASSWORD]} item={{ login, password }} labelCol={{ span: 6 }}>
			<ValidatedInput keyName={LOGIN} title="Логин" onChange={(_, n) => setLogin(n)} isRequired />
			<ValidatedInput
				keyName={PASSWORD}
				title="Пароль"
				type="password"
				onChange={(_, n) => setPassword(n)}
				isRequired
			/>
			<Button type="primary" className="float-right" title="Отправить" onClick={submit}>
				Отправить
			</Button>
		</Form>
	);
};

export default ContentForm;
