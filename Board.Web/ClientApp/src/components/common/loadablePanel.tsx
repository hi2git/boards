import React from "react";

import { AlertDanger, Spinner } from ".";

interface IProps {
	isLoading: boolean;
	size?: "small" | "normal";
	error?: string | Array<string | null> | null;
}

const LoadablePanel: React.FC<IProps> = ({ isLoading, error, size, children }) => {
	//console.log("error", error, error as Array<string>);
	// const errors = error instanceof Array && (error as Array<string>)?.filter(n => n);
	const isError = error instanceof Array ? (error as Array<string>)?.filter(n => n)?.length > 0 : error != null;
	return isLoading ? (
		<Spinner size={size} />
	) : (
		<>
			<AlertDanger value={error} />
			<Result isError={isError}>{children}</Result>
		</>
	);
};

interface IResultProps {
	isError: boolean;
}

export const Result: React.FC<IResultProps> = ({ isError, children }) => <>{!isError && children}</>;

export default LoadablePanel;
