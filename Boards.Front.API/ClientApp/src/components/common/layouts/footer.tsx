import React from "react";
import { Layout } from "antd";
import { LayoutProps } from "antd/lib/layout";

const { Footer: Wrapped } = Layout;

interface IProps extends LayoutProps {}

const Footer: React.FC<IProps> = props => <Wrapped className="text-center" {...props} />;

export default Footer;
