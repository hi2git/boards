import React from "react";
import { inject, observer } from "mobx-react";

import nameof from "../../utils/nameof";
import settings from "../../reducers/settings";
import { IUserSettings } from "../../interfaces/components";
import { Form, ValidatedInput } from "../common/forms";
import { FormInstance } from "antd/lib/form";
import { Button } from "../common";

const OLD_PASSWORD = nameof<IUserSettings>("oldPassword");
const NEW_PASSWORD = nameof<IUserSettings>("newPassword");
const CONFIRM_PASSWORD = nameof<typeof settings>("confirmPassword");
const keys = [OLD_PASSWORD, NEW_PASSWORD, CONFIRM_PASSWORD];

interface IProps {}

const Content: React.FC<IProps> = ({}) => {
	const ref = React.useRef<FormInstance>(null);

	const validatePasswordConfirm = (rule: any, value: string, callback: (msg?: string) => void) => {
		const msg = settings.isPasswordChanged && settings.isPasswordError ? rule.message : undefined;
		callback(msg);
	};

	return (
		<div className="settings">
			<Form ref={ref} item={settings} keys={keys} labelCol={{ span: 7 }}>
				<ValidatedInput
					title="Старый пароль"
					keyName={OLD_PASSWORD}
					type="password"
					isRequired
					onChange={(_, v) => settings.setOldPassword(v)}
				/>
				<ValidatedInput
					title="Новый пароль"
					keyName={NEW_PASSWORD}
					type="password"
					isRequired
					onChange={(_, v) => settings.setNewPassword(v)}
				/>
				<ValidatedInput
					title="Подтвердите пароль"
					keyName={CONFIRM_PASSWORD}
					type="password"
					isRequired
					rules={[{ validator: validatePasswordConfirm, message: "Пароли не совпадают" }]}
					onChange={(_, v) => settings.setConfirmPassword(v)}
				/>

				<Button
					type="primary"
					className="float-right"
					title="OK"
					disabled={!settings.isAllowSend}
					onClick={settings.send}
				>
					OK
				</Button>
			</Form>
		</div>
	);
};

export default inject("settings")(observer(Content));
