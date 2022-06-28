import React from "react";
import { observer } from "mobx-react";

import { CheckButton } from "../common";
import store from "../../reducers/boardSidebar";


interface IProps {}

const Control: React.FC<IProps> = () => {
	const { value, toggle, mount } = store;
	React.useEffect(mount, [mount]);

	return (
		<>
			<CheckButton
				title="Показать доски"
				type={value ? "primary" : "default"}
				onClick={toggle}
				ghost={value}
				skipFocus={!value}
				stopPropagation={false}
			>
				<i className="fas fa-bars" />
			</CheckButton>
		</>
	);
};

export default observer(Control);
