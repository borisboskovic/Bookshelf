import { createStore } from "vuex";
import AuthModule from "./auth-module";

export default createStore({
	modules: {
		auth: AuthModule,
	},
});
