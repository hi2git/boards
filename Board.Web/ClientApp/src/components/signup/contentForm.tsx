import React from "react";
import { observer } from "mobx-react";
import { FormInstance } from "antd/lib/form";

import store from "../../reducers/signup";
import nameof from "../../utils/nameof";
import Button from "../common/button";
import { Form, ValidatedInput } from "../common/forms";

const NAME = nameof<typeof store.item>("login");
const PASSWORD = nameof<typeof store.item>("password");
const keys = [NAME, PASSWORD];

interface IProps {}

const Content: React.FC<IProps> = () => {
	const ref = React.useRef<FormInstance>(null);

	const { item, isAllowSend, set, send } = store;

	return (
		<Form ref={ref} item={item} keys={keys} labelCol={{ span: 7 }}>
			<ValidatedInput title="Логин" keyName={NAME} isRequired onChange={set} />
			<ValidatedInput title="Пароль" keyName={PASSWORD} isRequired onChange={set} />

			<Button
				type="primary"
				className="float-right"
				title="Зарегистрироваться"
				disabled={!isAllowSend}
				onClick={send}
			>
				Зарегистрироваться
			</Button>
		</Form>
	);
};

export default observer(Content);
