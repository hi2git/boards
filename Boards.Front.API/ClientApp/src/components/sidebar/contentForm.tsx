import React from "react";
import { Button, InputGroup } from "../common";
import { Form, FormItem, useForm, ValidatedInput } from "../common/forms";

import boards from "../../reducers/boards";

const NAME = "name";

interface IProps {}

const ContentForm: React.FC<IProps> = () => {
	const form = useForm();

	const [name, setName] = React.useState("");

	return (
		<div className="row">
			<div className="col-12 px-0">
				<Form
					form={form}
					keys={[NAME]}
					item={{ name }}
					onFinish={() => boards.post(name)}
					layout="inline"
					className=" w-100"
				>
					<div className="w-90">
						<ValidatedInput
							title="Название новой доски"
							keyName={NAME}
							max={50}
							isInline
							onChange={(_, v) => setName(v)}
						/>
					</div>
					<div className="w-10">
						<FormItem>
							<Button title="Добавить" htmlType="submit" type="link" disabled={!name}>
								<i className="fas fa-check" />
							</Button>
						</FormItem>
					</div>
				</Form>
			</div>
		</div>
	);
};

export default ContentForm;
