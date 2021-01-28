import { createBrowserHistory, History } from "history";
import configureStore from "./configureStore";

// Create browser history to use in the Redux store
const basename = document.getElementsByTagName("base")[0].getAttribute("href") ?? undefined;
export const history: History = createBrowserHistory();

// Get the application-wide store instance, prepopulating with state from the server where available.
// const initialState :  = window.initialReduxState;

export const store = configureStore(history, {});
