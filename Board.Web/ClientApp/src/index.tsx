import React from "react";
import ReactDOM from "react-dom";
import { syncHistoryWithStore } from "mobx-react-router";
import { Router } from "react-router";
import { createBrowserHistory } from "history";

import App from "./components/apps/app";

import store from "./reducers/router";

import "bootstrap/dist/css/bootstrap.css";
import "@fortawesome/fontawesome-free/css/all.min.css";
import "antd/dist/antd.css";
import "./index.css";

const browserHistory = createBrowserHistory();
const history = syncHistoryWithStore(browserHistory, store.router);

ReactDOM.render(
	<Router history={history}>
		<App />
	</Router>,
	document.getElementById("root")
);
