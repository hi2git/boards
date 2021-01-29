import React from "react";
import { Slider as Wrapped } from "antd";
import { SliderSingleProps } from "antd/lib/slider";

interface IProps extends SliderSingleProps {}

const Slider: React.FC<IProps> = props => <Wrapped min={0.1} max={1} step={0.05} {...props} />;

export default Slider;
