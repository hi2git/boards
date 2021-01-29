import React from "react";
import { Button as Btn } from "antd";
import { ButtonProps } from "antd/lib/button";

import { Tooltip } from ".";

export interface IButtonProps extends ButtonProps {
	title: string;
	skipFocus?: boolean;
	stopPropagation?: boolean;
}

const Button: React.FC<IButtonProps> = ({ title, style, children, skipFocus, stopPropagation = true, ...props }) => {
	const st = skipFocus ? { color: "inherit", borderColor: "#d9d9d9" } : undefined;
	return (
		<Tooltip title={title}>
			<Btn
				style={{ opacity: "80%", ...st, ...style }}
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
