import React from "react";
import { Menu } from "antd";
import { MenuItemProps } from "antd/lib/menu/MenuItem";

interface IProps extends MenuItemProps {}

const MenuItem: React.FC<IProps> = ({ children, ...props }) => <Menu.Item {...props}>{children}</Menu.Item>;

export default MenuItem;
