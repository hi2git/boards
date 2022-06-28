import React from "react";
import { Button as Btn } from "antd";
import { ButtonProps } from "antd/lib/button";

import { Tooltip } from ".";

export interface IButtonProps extends ButtonProps {
	title: string;
	stopPropagation?: boolean;
}

const Button: React.FC<IButtonProps> = ({ title, style, children, stopPropagation = true, ...props }) => {
	return (
		<Tooltip title={title}>
			<Btn
				style={{ opacity: "80%", ...style }}
				type="default"
				onMouseDown={e => {
					if (stopPropagation) e.stopPropagation();
				}}
				onTouchStart={e => {
					if (stopPropagation) {
						e.stopPropagation();
						props.onClick?.(e as any);
					}
				}}
				{...props}
			>
				{children}
			</Btn>
		</Tooltip>
	);
};

export default Button;
