import React from "react";
import { Input } from "antd";
import { GroupProps } from "antd/lib/input/Group";

interface IProps extends GroupProps {}

const InputGroup: React.FC<IProps> = props => <Input.Group compact {...props}></Input.Group>;

export default InputGroup;
