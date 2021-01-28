import axios, { AxiosRequestConfig, AxiosResponse } from "axios";

import * as urls from "../constants/urls";

const api = axios.create();

api.interceptors.request.use(request => requestInterceptor(request));
axios.defaults.headers["Pragma"] = "no - cache";
api.interceptors.response.use(
	response => successHandler(response),
	error => errorHandler(error)
);

const requestInterceptor = (request: AxiosRequestConfig) => {
	request.withCredentials = true;
	request.headers["Pragma"] = "no-cache";
	return request;
};

const successHandler = (response: AxiosResponse<any>) => {
	return new Promise<AxiosResponse<any>>((resolve, _) => {
		if (response.status === 200 || response.status === 204) {
			resolve(response);
		}
	});
};

const errorHandler = (error: any) =>
	new Promise((_, reject) => {
		if (error.response.status === 401 || error.response.status === 403) {
			window.location.href = urls.LOGIN; // TODO: SPA redirect
		}

		if (error.response.status === 401 || error.response.status === 403) {
			if ([401, 403].indexOf(error.response.status) !== -1) {
				//authActions.logout();
			}
			reject();
		}

		return reject(error.response.data.message);
	});

export default api;
