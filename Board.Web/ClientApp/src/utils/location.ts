import * as urls from "../constants/urls";

const getUrl = () => {
	const path = "/" + window.location.pathname.split("/")[1].toLowerCase();
	switch (path) {
		case urls.SETTINGS:
			return urls.SETTINGS;
		case urls.CONTACTS:
			return urls.CONTACTS;
		default:
			return urls.HOME;
	}
};
export default getUrl;
