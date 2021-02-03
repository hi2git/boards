import React from "react";
import { observer } from "mobx-react";

import * as urls from "../../constants/urls";
import router from "../../reducers/router";
import { Menu } from "../common";

import Home from "./contentHome";
import Contacts from "./contentContacts";
import Logout from "./contentLogout";
import Settings from "./contentSettings";
import Title from "./contentTitle";

interface IProps {}

const Content: React.FC<IProps> = () => {
	const { startPath } = router;
	const [selected, setSelected] = React.useState<string>(startPath);

	const select = async (key: string) => {
		key = key === "none" ? urls.HOME : key;
		await setSelected(key);
	};

	return (
		<div className="header">
			<Menu
				className="text-center"
				defaultOpenKeys={[urls.HOME]}
				mode="horizontal"
				selectedKeys={selected ? [selected] : undefined}
				onClick={e => select(e.key as string)}
			>
				<Home key={urls.HOME} />

				<Title key="none" />

				<Logout key="logout" />
				<Contacts key={urls.CONTACTS} />
				<Settings key={urls.SETTINGS} />
			</Menu>
		</div>
	);
};

export default observer(Content);
