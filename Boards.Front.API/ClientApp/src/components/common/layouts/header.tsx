import React from "react";
import { Layout } from "antd";
import { LayoutProps } from "antd/lib/layout";

const { Header: Wrapped } = Layout;

interface IProps extends LayoutProps {}

const Header: React.FC<IProps> = props => <Wrapped {...props} />;

export default Header;
