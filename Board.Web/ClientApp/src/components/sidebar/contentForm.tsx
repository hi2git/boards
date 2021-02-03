import React from "react";
import { Button } from "../common";
import { Form, FormItem, isValidateError, useForm, ValidatedInput } from "../common/forms";

import boards from "../../reducers/boards";

const NAME = "name";

interface IProps {}

const ContentForm: React.FC<IProps> = () => {
	const form = useForm();

	const [isError, setError] = React.useState(true);
	const [name, setName] = React.useState("");

	return (
		<Form form={form} keys={[NAME]} item={{ name }} onFinish={() => boards.post(name)} layout="inline">
			<ValidatedInput
				className="mx-0"
				title="Название"
				keyName={NAME}
				isRequired
				isInline
				onChange={async (_, v) => {
					await setName(v);
					setError(await isValidateError(form));
				}}
			/>
			<FormItem className="mx-0">
				<Button title="Добавить" htmlType="submit" disabled={isError}>
					<i className="fas fa-plus" />
				</Button>
			</FormItem>
		</Form>
	);
};

export default ContentForm;
