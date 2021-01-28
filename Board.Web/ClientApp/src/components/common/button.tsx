// import React from "react";
// import { Tooltip } from ".";

// interface IProps {
// 	className?: string;
// 	isDisabled?: boolean;
// 	style?: React.CSSProperties;
// 	type?: "primary" | "secondary" | "link" | "light" | "danger";
// 	isOutline?: boolean;
// 	isLarge?: boolean;
// 	title?: string;
// 	onClick: () => void;
// }

// const Button: React.FC<IProps> = props => {
// 	const {
// 		style,
// 		isDisabled,
// 		className = "",
// 		isOutline,
// 		isLarge,
// 		onClick,
// 		children,
// 		type = "secondary",
// 		title = "",
// 	} = props;

// 	const outline = isOutline ? "outline-" : "";
// 	const cls = `${outline}${type}`;
// 	const large = isLarge ? "btn-lg" : "";

// 	return (
// 		<Tooltip title={title}>
// 			<button
// 				style={{ opacity: "80%", ...style }}
// 				disabled={isDisabled}
// 				className={`${className} btn btn-${cls} ${large}`}
// 				onClick={onClick}
// 				onMouseDown={e => e.stopPropagation()}
// 			>
// 				{children}
// 			</button>
// 		</Tooltip>
// 	);
// };

// export default Button;

import React from "react";
import { Button as Btn } from "antd";
import { ButtonProps } from "antd/lib/button";

import { Tooltip } from ".";

export interface IButtonProps extends ButtonProps {
	title: string;
}

const Button: React.FC<IButtonProps> = ({ title, style, children, ...props }) => (
	<Tooltip title={title}>
		<Btn
			style={{ opacity: "80%", ...style }}
			type="default"
			onMouseDown={e => e.stopPropagation()}
			onTouchStart={e => {
				e.stopPropagation();
				props.onClick?.(e as any);
			}}
			{...props}
		>
			{children}
		</Btn>
	</Tooltip>
);

export default Button;
