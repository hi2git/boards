import React from "react";
import { AlertPanel } from ".";

interface IProps {
	value?: string | Array<string | null> | null;
}

const Alert: React.FC<IProps> = ({ value }) => {
	const isArray = value instanceof Array;
	const values = isArray ? (value as Array<string>)?.filter(n => n) : null;

	const content = isArray ? values?.map(n => <li key={n}>{n}</li>) : value;
	const result = isArray ? <ul>{content}</ul> : content;

	const isEmpty = (isArray && values?.length === 0) || !value;
	return isEmpty ? null : (
		<AlertPanel isVisible={value?.length !== 0 || value == null} type="danger">
			{result}
		</AlertPanel>
	);
};

export default Alert;
