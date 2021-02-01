import React from "react";
import { Slider as Wrapped } from "antd";
import { SliderSingleProps } from "antd/lib/slider";

interface IProps extends SliderSingleProps {}

const Slider: React.FC<IProps> = props => <Wrapped min={10} max={100} step={5} {...props} />;

export default Slider;
