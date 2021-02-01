import React from "react";
import { observer } from "mobx-react";

import { Slider } from "../common";
import scale from "../../reducers/boardScale";

interface IProps {}

const Toggler: React.FC<IProps> = () => {
	const { value, setValue, mount } = scale;
	React.useEffect(mount, [mount]);

	return (
		<div className="border float-right w-25 mr-2 px-2">
			<Slider value={value} tipFormatter={n => `${n}%`} onChange={setValue} />
		</div>
	);
};

export default observer(Toggler);
