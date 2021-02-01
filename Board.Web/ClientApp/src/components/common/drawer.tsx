import React from "react";
import { Drawer as Wrapped } from "antd";
import { DrawerProps } from "antd/lib/drawer";

interface IProps extends DrawerProps {}

const Drawer: React.FC<IProps> = props => <Wrapped width="20%" closable={false} placement="left" {...props} />;

export default Drawer;
