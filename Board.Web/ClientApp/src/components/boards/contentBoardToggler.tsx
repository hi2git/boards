import React from "react";
import { observer } from "mobx-react";

import { CheckButton } from "../common";
import store from "../../reducers/boardControl";

interface IProps {}

const Toggler: React.FC<IProps> = () => {
	const { isVisible, toggleVisible } = store;
	return (
		<CheckButton
			title="Показать управление"
			type={isVisible ? "primary" : "default"}
			onClick={toggleVisible}
			ghost={isVisible}
			skipFocus={!isVisible}
			stopPropagation={false}
		>
			<i className="fas fa-eye" />
		</CheckButton>
	);
};

export default observer(Toggler);
