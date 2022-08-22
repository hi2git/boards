import React from "react";
import { Switch, Route } from "react-router";

import { Content, Layout } from "../common/layouts";
import * as urls from "../../constants/urls";
import { Login, SignUp, Boards, Contacts, Settings } from "../pages";
import Header from "../header/content";
import Footer from "../footer/content";

import "./app.css";

const App: React.FC = () => (
	<div className="app container-fluid">
		<Switch>
			<Route strict path={urls.LOGIN} component={Unauthorized} />
			<Route strict path={urls.SIGNUP} component={Unauthorized} />
			<Route path={urls.HOME} component={Container} />
		</Switch>
	</div>
);

const Unauthorized: React.FC = () => {
	return (
		<div className="row" style={{ marginTop: "20%" }}>
			<div className="offset-sm-3 col-sm-6">
				<Switch>
					<Route strict path={urls.LOGIN} component={Login} />
					<Route strict path={urls.SIGNUP} component={SignUp} />
				</Switch>
			</div>
		</div>
	);
};

const Container: React.FC = () => (
	<Layout>
		<Header />
		<Content>
			<Switch>
				<Route strict path={urls.CONTACTS} component={Contacts} />
				<Route strict path={urls.SETTINGS} component={Settings} />
				<Route path={urls.HOME} component={Boards} />
			</Switch>
		</Content>
		{/* <Footer /> */}
	</Layout>
);

export default App;
