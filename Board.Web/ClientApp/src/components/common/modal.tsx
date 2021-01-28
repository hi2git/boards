import React from "react";
import { Modal as Mdl } from "antd";
import { ModalProps } from "antd/lib/modal";

interface IProps extends ModalProps {
	// isVisible: boolean;
}

const Modal: React.FC<IProps> = props => {
	return (
		<Mdl
			width="40%"
			destroyOnClose
			{...props}
			onOk={e => {
				e.stopPropagation();
				props.onOk?.(e);
			}}
			onCancel={e => {
				e.stopPropagation();
				props.onCancel?.(e);
			}}
		/>
	);
};

export default Modal;
