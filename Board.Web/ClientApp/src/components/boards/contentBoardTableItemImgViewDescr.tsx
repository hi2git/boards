import { observer } from "mobx-react";
import React, { useState } from "react";
import { Button, Modal, TextArea } from "../common";
import { IBoardItem } from "../../interfaces/components";

import store from "../../reducers/boardItem";

interface IProps {
	item: IBoardItem;
	onChange: (value?: string) => void;
}

const DescriptionDlg: React.FC<IProps> = ({ item, onChange }) => {
	const [val, setVal] = useState(item.description);

	const { value: id, setValue, mount } = store;
	React.useEffect(mount, [mount]);

	return (
		<>
			<Button title="Описание" onClick={() => setValue(item.id)}>
				<i className="fas fa-hashtag" />
			</Button>
			<Modal
				title="Описание"
				visible={id === item.id}
				onOk={() => {
					setValue();
					onChange(val);
				}}
				onCancel={() => setValue()}
			>
				<TextArea value={val} autoFocus onChange={setVal} />
			</Modal>
		</>
	);
};

export default observer(DescriptionDlg);
