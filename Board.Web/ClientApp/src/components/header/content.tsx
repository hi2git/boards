import React, { useEffect, useState } from "react";
import { Menu } from "../common";

import Home from "./contentHome";
import Contacts from "./contentContacts";
import Logout from "./contentLogout";

interface IProps {}

const Content: React.FC<IProps> = ({}) => {
	const [selected, setSelected] = useState<string>();

	useEffect(() => {
		setSelected("home");
	}, []);

	return (
		<div className="header">
			<Menu
				defaultOpenKeys={["home"]}
				mode="horizontal"
				selectedKeys={selected ? [selected] : undefined}
				onClick={e => setSelected(e.key as string)}
			>
				<Home key="home" />
				<Logout key="logout" />
				<Contacts key="contacts" />
			</Menu>
		</div>
	);
};

export default Content;
