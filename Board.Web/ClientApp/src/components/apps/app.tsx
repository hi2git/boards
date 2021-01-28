import React from "react";
import { Switch, Route } from "react-router";

import { Login, Boards, Contacts } from "../pages";
import Header from "../header/content";

import "./app.css";

const App: React.FC = () => {
	return (
		<div className="app">
			<div className="container py-2">
				<Switch>
					<Route strict path="/login" component={Login} />
					<Route path="/" component={Container} />
				</Switch>
			</div>
		</div>
	);
};

const Container: React.FC = () => {
	return (
		<>
			<Header />
			<Switch>
				<Route strict path="/contacts" component={Contacts} />
				<Route path="/" component={Boards} />
			</Switch>
		</>
	);
};

export default App;
