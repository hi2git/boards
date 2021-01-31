import React from "react";
import { observer } from "mobx-react";

import { Slider } from "../common";
import scale from "../../reducers/boardScale";

interface IProps {}

const Toggler: React.FC<IProps> = () => {
	const { value, setValue, mount } = scale;
	React.useEffect(mount, []);

	return <Slider value={value} onChange={setValue} />;
};

export default observer(Toggler);
