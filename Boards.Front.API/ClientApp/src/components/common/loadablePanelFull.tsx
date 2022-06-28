import React from "react";

import { AlertDanger, Modal, Spinner } from ".";

interface IProps {
	isLoading: boolean;
	size?: "small" | "normal";
	error?: string | Array<string | null> | null;
}

const LoadablePanel: React.FC<IProps> = ({ isLoading, error, size, children }) => {
	const isError = error instanceof Array ? (error as Array<string>)?.filter(n => n)?.length > 0 : error != null;
	return (
		<>
			{isLoading && <SpinnerCover />}
			<AlertDanger value={error} />
			<Result isError={isError}>{children}</Result>
		</>
	);
};

interface IResultProps {
	isError: boolean;
}

export const Result: React.FC<IResultProps> = ({ isError, children }) => <>{!isError && children}</>;

interface ISpinnerCoverProps {
	// isVisible: boolean;
}

const SpinnerCover: React.FC<ISpinnerCoverProps> = () => (
	<>
		<div className="ant-modal-mask" style={{ opacity: 0.3 }} />
		<div className="ant-modal-wrap" style={{ marginTop: "15%" }}>
			<div className="ant-modal" style={{ width: "10%" }}>
				<Spinner style={{ color: "white" }} />
			</div>
		</div>
	</>
);

export default LoadablePanel;
