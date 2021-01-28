import React, { useEffect, useState } from "react";

import * as urls from "../../constants/urls";
import { Menu } from "../common";

import Home from "./contentHome";
import Contacts from "./contentContacts";
import Logout from "./contentLogout";
import Settings from "./contentSettings";

interface IProps {}

const Content: React.FC<IProps> = ({}) => {
	const [selected, setSelected] = useState<string>();

	useEffect(() => {
		setSelected(window.location.pathname);
	}, []);

	return (
		<div className="header">
			<Menu
				defaultOpenKeys={[urls.HOME]}
				mode="horizontal"
				selectedKeys={selected ? [selected] : undefined}
				onClick={e => setSelected(e.key as string)}
			>
				<Home key={urls.HOME} />

				<Logout key="logout" />
				<Contacts key={urls.CONTACTS} />
				<Settings key={urls.SETTINGS} />
			</Menu>
		</div>
	);
};

export default Content;
