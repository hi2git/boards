import React from "react";
import { observer } from "mobx-react";

import { Slider } from "../common";
import store from "../../reducers/boardScale";

interface IProps {}

const Toggler: React.FC<IProps> = () => {
	const { scale, setScale } = store;
	return <Slider value={scale} onChange={setScale} />;
};

export default observer(Toggler);
