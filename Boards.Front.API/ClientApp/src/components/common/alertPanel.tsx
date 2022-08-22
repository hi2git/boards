import React from "react";

interface IProps {
	type: "danger" | "info";
	isVisible: boolean;
}

const AlertPanel: React.FC<IProps> = ({ children, isVisible, type }) => {
	const className = `alert alert-${type}`;
	return !isVisible ? null : (
		<div className="row">
			<div className="col-12 px-0">
				<div className={className} style={{ maxHeight: "100px", overflow: "auto" }}>
					{children}
				</div>
			</div>
		</div>
	);
};

export default AlertPanel;
