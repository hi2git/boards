import React from "react";
import { Provider } from "react-redux";
import ReactDOM from "react-dom";
import { ConnectedRouter } from "connected-react-router";

import App from "./components/apps/app";

import { store, history } from "./store/store";

import "bootstrap/dist/css/bootstrap.css";
import "@fortawesome/fontawesome-free/css/all.min.css";
import "antd/dist/antd.css";
import "./index.css";

ReactDOM.render(
	<Provider store={store}>
		<ConnectedRouter history={history}>
			<App />
		</ConnectedRouter>
	</Provider>,
	document.getElementById("root")
);
