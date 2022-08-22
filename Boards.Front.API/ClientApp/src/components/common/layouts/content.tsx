import React from "react";
import { Layout } from "antd";
import { LayoutProps } from "antd/lib/layout";

const { Content: Wrapped } = Layout;

interface IProps extends LayoutProps {}

const Content: React.FC<IProps> = props => <Wrapped {...props} />;

export default Content;
