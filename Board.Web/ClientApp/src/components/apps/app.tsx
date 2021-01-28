import React from "react";
import { Switch, Route } from "react-router";

import * as urls from "../../constants/urls";
import { Login, SignUp, Boards, Contacts, Settings } from "../pages";
import Header from "../header/content";

import "./app.css";

const App: React.FC = () => (
	<div className="app">
		<div className="container py-2">
			<Switch>
				<Route strict path={urls.LOGIN} component={Unauthorized} />
				<Route strict path={urls.SIGNUP} component={Unauthorized} />
				<Route path={urls.HOME} component={Container} />
			</Switch>
		</div>
	</div>
);

const Unauthorized: React.FC = () => {
	return (
		<div className="row" style={{ marginTop: "30%" }}>
			<div className="offset-2 col-8">
				<Switch>
					<Route strict path={urls.LOGIN} component={Login} />
					<Route strict path={urls.SIGNUP} component={SignUp} />
				</Switch>
			</div>
		</div>
	);
};

const Container: React.FC = () => (
	<>
		<Header />
		<Switch>
			<Route strict path={urls.CONTACTS} component={Contacts} />
			<Route strict path={urls.SETTINGS} component={Settings} />
			<Route path={urls.HOME} component={Boards} />
		</Switch>
	</>
);

export default App;
