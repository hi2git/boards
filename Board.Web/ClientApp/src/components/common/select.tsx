// import React from "react";

// import { IIdName } from "../../interfaces/components";
// import LoadablePanel from "./loadablePanel";

// interface IProps {
// 	isLoading: boolean;
// 	options: Array<IIdName>;
// 	onChange: (id: string) => Promise<void>;
// }

// const Select: React.FC<IProps> = ({ isLoading, options, onChange }) => {
// 	const opts = options.map(n => (
// 		<option key={n.id} value={n.id}>
// 			{n.name}
// 		</option>
// 	));
// 	return (
// 		<LoadablePanel isLoading={isLoading}>
// 			<select className="form-control" onChange={e => onChange(e.target.value)}>
// 				{opts}
// 			</select>
// 		</LoadablePanel>
// 	);
// };

// export default Select;

import React from "react";
import { Select as Slct } from "antd";
import { SelectProps } from "antd/lib/select";
import { IIdName } from "../../interfaces/components";

interface IProps extends SelectProps<any> {
	opts: Array<IIdName>;
}

const Select: React.FC<IProps> = ({ opts, ...otherProps }) => {
	const options = opts?.map(n => (
		<Slct.Option key={n.id} value={n.id} name={n.name}>
			{n.name}
		</Slct.Option>
	));
	return (
		<Slct allowClear showSearch optionFilterProp="name" getPopupContainer={n => n.parentElement} {...otherProps}>
			{options}
		</Slct>
	);
};

export default Select;
