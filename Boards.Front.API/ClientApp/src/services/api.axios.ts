import axios, { AxiosRequestConfig, AxiosResponse } from "axios";

import * as urls from "../constants/urls";
import router from "../reducers/router";

const api = axios.create();

api.interceptors.request.use(request => requestInterceptor(request));
axios.defaults.headers["Pragma"] = "no-cache";
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
		if (error.response.status === 400) {
			if (!!error.response.data.errors) {
				const entries = Object.entries(error.response.data.errors).map(n => `${n[0]} - ${n[1]}`);
				const msg = entries.join();
				console.error(msg);
				reject(msg);
			}
		}

		if (error.response.status === 401 || error.response.status === 403) {
			router.push(urls.LOGIN);
			reject("Пожалуйста, авторизуйтесь");
		}
		if (error.response.status === 404) {
			reject("Не найдено");
		}

		return reject(error.response.data.message);
	});

export default api;
