import React from "react";
import { Pagination } from "antd";
import { PaginationProps } from "antd/lib/pagination";

export const defaultPageSizes = ["6", "12", "24", "51"];
// export const defaultClass = "mb-3";  // className={defaultClass}

interface IProps extends PaginationProps {}

const Paging: React.FC<IProps> = ({ total, ...props }) =>
	total === 0 ? null : <Pagination total={total} pageSizeOptions={defaultPageSizes} showSizeChanger {...props} />;

export default Paging;
