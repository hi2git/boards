import React from "react";
import { Menu as Mnu } from "antd";
import { MenuProps } from "antd/lib/menu";

interface IProps extends MenuProps {}

const Menu: React.FC<IProps> = ({ children, ...props }) => <Mnu {...props}>{children}</Mnu>;

export default Menu;
