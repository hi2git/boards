import React from "react";
import { Layout as Wrapped } from "antd";
import { LayoutProps } from "antd/lib/layout";

interface IProps extends LayoutProps {}

const Layout: React.FC<IProps> = props => <Wrapped {...props} />;

export default Layout;
