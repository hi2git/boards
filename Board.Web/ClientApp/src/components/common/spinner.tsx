import React from "react";

interface IProps {
	size?: "small" | "normal";
}

const Spinner: React.FC<IProps> = ({ size = "normal" }) => (size === "normal" ? <NormalSpinner /> : <SmallSpinner />);

export const NormalSpinner: React.FC = () => (
	<div className="row my-2">
		<div className="col-12">
			<div className="d-flex justify-content-center">
				<SmallSpinner />
			</div>
		</div>
	</div>
);

export const SmallSpinner: React.FC = () => <span className="spinner-border text-secondary" />;

export default Spinner;
