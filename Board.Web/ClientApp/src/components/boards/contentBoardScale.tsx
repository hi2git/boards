import React from "react";
import { observer } from "mobx-react";

import { Slider } from "../common";
import store from "../../reducers/boardScale";

interface IProps {}

const Toggler: React.FC<IProps> = () => <Slider value={store.value} onChange={store.setScale} />;

export default observer(Toggler);
