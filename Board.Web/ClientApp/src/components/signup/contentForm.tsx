import React from "react";
import { Link } from "react-router-dom";
import { observer } from "mobx-react";
import { FormInstance } from "antd/lib/form";
import { Captcha, captchaSettings } from "reactjs-captcha";

import store from "../../reducers/signup";
import nameof from "../../utils/nameof";
import * as urls from "../../constants/urls";
import Button from "../common/button";
import { Form, FormItem, ValidatedInput } from "../common/forms";
import { Input } from "../common";

const NAME = nameof<typeof store.item>("login");
const PASSWORD = nameof<typeof store.item>("password");
const EMAIL = nameof<typeof store.item>("email");
const keys = [NAME, PASSWORD, EMAIL];

captchaSettings.set({
	captchaEndpoint: "captcha.ashx",
});

interface IProps {}

const Content: React.FC<IProps> = () => {
	const ref = React.useRef<FormInstance>(null);
	const captchaRef = React.useRef<any>();

	const { item, isAllowSend, set, send } = store;

	const submit = async () => {
		const captchaCode = captchaRef.current?.getUserEnteredCaptchaCode();
		const captchaId = captchaRef.current?.getCaptchaId();
		set("captchaCode", captchaCode);
		set("captchaId", captchaId);
		// console.log("captchaCode", captchaCode, "captchaId", captchaId);
		await send();
		captchaRef.current?.reloadImage();
	};

	return (
		<Form ref={ref} item={item} keys={keys} labelCol={{ span: 4 }} onFinish={submit}>
			<ValidatedInput
				autoComplete="new-password"
				title="Логин"
				keyName={NAME}
				max={50}
				isRequired
				autoFocus
				onChange={set}
			/>
			<ValidatedInput title="Пароль" keyName={PASSWORD} max={50} isRequired type="password" onChange={set} />
			<ValidatedInput title="Email" keyName={EMAIL} max={50} isRequired type="email" onChange={set} />

			<FormItem label="Введите символы">
				<Input id="captchaInput" type="text" maxLength={6} />
				<div className="mt-2">
					<Captcha captchaStyleName="captcha" ref={captchaRef} />
				</div>
			</FormItem>
			<Button
				type="primary"
				className="float-right"
				title="Зарегистрироваться"
				disabled={!isAllowSend}
				htmlType="submit"
			>
				Зарегистрироваться
			</Button>
			<Button type="link" className="float-right" title="Назад">
				<Link className="float-right" to={urls.LOGIN}>
					Назад
				</Link>
			</Button>
		</Form>
	);
};

export default observer(Content);
