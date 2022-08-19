import React from "react";
import { Pagination } from "antd";
import { PaginationProps } from "antd/lib/pagination";

export const defaultPageSizes = ["6", "12", "24", "51"];
// export const defaultClass = "mb-3";  // className={defaultClass}

interface IProps extends PaginationProps {}

const Paging: React.FC<IProps> = props => <Pagination pageSizeOptions={defaultPageSizes} showSizeChanger {...props} />;

export default Paging;
