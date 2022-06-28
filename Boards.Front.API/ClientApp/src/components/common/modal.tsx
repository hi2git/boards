import React from "react";
import { Modal as Mdl } from "antd";
import { ModalFuncProps, ModalProps } from "antd/lib/modal";

export const confirm = (props: ModalFuncProps) => Mdl.confirm(props);

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
