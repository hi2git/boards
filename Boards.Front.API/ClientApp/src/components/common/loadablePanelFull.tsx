import React from "react";

import { AlertDanger, Spinner } from ".";

interface IProps {
	isLoading: boolean;
	size?: "small" | "normal";
	error?: string | Array<string | null> | null;
}

const LoadablePanelFull: React.FC<IProps> = ({ isLoading, error, children }) => {
	// const isError = error instanceof Array ? (error as Array<string>)?.filter(n => n)?.length > 0 : error != null;
	return (
		<>
			{isLoading && <SpinnerCover />}
			<AlertDanger value={error} />
			{/* <Result isError={isError}>{children}</Result> */}
			{children}
		</>
	);
};

// interface IResultProps {
// 	isError: boolean;
// }

// export const Result: React.FC<IResultProps> = ({ isError, children }) => <>{!isError && children}</>;

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

export default LoadablePanelFull;
