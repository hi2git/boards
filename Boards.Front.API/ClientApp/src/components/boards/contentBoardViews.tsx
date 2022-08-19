import React from "react";
import { observer } from "mobx-react";

import { RadioGroup } from "../common";

import view from "../../reducers/view";
interface IProps {}

const Views: React.FC<IProps> = () => {
	const { value, setValue, mount } = view;
	React.useEffect(mount, [mount]);

	const btns = [
		{ id: "cover", name: "Центрировать", icon: "arrows-alt" },
		{ id: "fill", name: "По ширине", icon: "arrows-alt-h" },
		{ id: "scale-down", name: "По высоте", icon: "arrows-alt-v" },
	];

	const opts =
		btns.map(n => ({
			id: n.id,
			title: n.name,
			label: <i className={`fas fa-${n.icon}`} />,
		})) ?? [];

	return <RadioGroup value={value} opts={opts} onChange={e => setValue(e.target.value)} />;
};

export default observer(Views);
