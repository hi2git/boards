import React from "react";
import { observer } from "mobx-react";

import { RadioGroup } from "../common";

import store from "../../reducers/view";
interface IProps {}

const Views: React.FC<IProps> = () => {
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

	return (
		<RadioGroup
			defaultValue={store.selected}
			className="float-right"
			opts={opts}
			onChange={e => store.setSelected(e.target.value)}
		/>
	);
};

export default observer(Views);
