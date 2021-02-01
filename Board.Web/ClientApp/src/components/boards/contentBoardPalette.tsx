import React from "react";
import { observer } from "mobx-react";

import { CheckButton } from "../common";
import store from "../../reducers/boardPalette";

interface IProps {}

const Palette: React.FC<IProps> = () => {
	const { value, toggle, mount } = store;
	React.useEffect(mount, [mount]);

	return (
		<CheckButton
			title="Показать палитру"
			type={value ? "primary" : "default"}
			onClick={toggle}
			ghost={value}
			skipFocus={!value}
			stopPropagation={false}
		>
			<i className="fas fa-palette" />
		</CheckButton>
	);
};

export default observer(Palette);
