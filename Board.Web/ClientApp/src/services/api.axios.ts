import axios, { AxiosRequestConfig, AxiosResponse } from "axios";

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

const errorHandler = (error: any) => {
	return new Promise((_, reject) => {
		// TODO: Обрабатывайте тут свои ошибки

		if (error.response.status === 401 || error.response.status === 403) {
			// console.log("Deleting auth...");
			// authActions.logout();
			// sessionStorage.removeItem("auth");
			window.location.href = "/login";
		}

		// Invalid model
		if (error.response.status === 400) {
			// const messages = Object.values(error.response.data.errors).flatMap(n => n);
			// return reject(messages);
			return reject("Bad request");
		}
		// if (error.response.status === 401 || error.response.status === 403) {
		// 	if ([401, 403].indexOf(error.response.status) !== -1) {
		// 		//authActions.logout();
		// 	}
		// 	reject();
		// }

		return reject(error.response.data.message);
	});
};

export default api;
