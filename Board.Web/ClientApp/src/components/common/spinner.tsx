import React, { CSSProperties } from "react";

interface IStyleable {
	style?: CSSProperties;
}

interface IProps extends IStyleable {
	size?: "small" | "normal";
}

const Spinner: React.FC<IProps> = ({ size = "normal", style }) =>
	size === "normal" ? <NormalSpinner style={style} /> : <SmallSpinner style={style} />;

export const NormalSpinner: React.FC<IStyleable> = ({ style }) => (
	<div className="row my-2">
		<div className="col-12">
			<div className="d-flex justify-content-center">
				<SmallSpinner style={style} />
			</div>
		</div>
	</div>
);

export const SmallSpinner: React.FC<IStyleable> = ({ style }) => (
	<span className="spinner-border text-secondary" style={style} />
);

export default Spinner;
