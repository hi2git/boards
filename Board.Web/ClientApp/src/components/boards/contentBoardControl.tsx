import React from "react";
import { observer } from "mobx-react";

import { CheckButton } from "../common";
import control from "../../reducers/boardControl";

interface IProps {}

const Toggler: React.FC<IProps> = () => {
	const { value, toggle, mount } = control;
	React.useEffect(mount, []);

	return (
		<CheckButton
			title="Показать управление"
			type={value ? "primary" : "default"}
			onClick={toggle}
			ghost={value}
			skipFocus={!value}
			stopPropagation={false}
		>
			<i className="fas fa-eye" />
		</CheckButton>
	);
};

export default observer(Toggler);
