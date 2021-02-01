import { storeAnnotation } from "mobx/dist/internal";
import React from "react";
import { Button } from "../common";
import { Form, FormItem, isValidateError, useForm, ValidatedInput } from "../common/forms";

import store from "../../reducers/boards";
import { IIdName } from "../../interfaces/components";

const NAME = "name";

interface IProps {}

const ContentForm: React.FC<IProps> = () => {
	const form = useForm();

	const [item, setItem] = React.useState<IIdName>({ id: "", name: "" });
	const [isError, setError] = React.useState(true);

	return (
		<Form form={form} keys={[NAME]} item={item} onFinish={() => store.post(item)} layout="inline">
			<ValidatedInput
				title="Название"
				keyName={NAME}
				isRequired
				isInline
				onChange={async (k, v) => {
					await setItem({ ...item, [k]: v });
					setError(await isValidateError(form));
				}}
			/>
			<FormItem>
				<Button title="Добавить" htmlType="submit" disabled={isError}>
					<i className="fas fa-plus" />
				</Button>
			</FormItem>
		</Form>
	);
};

export default ContentForm;
