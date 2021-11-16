import { createStore } from "vuex";
import authModule from "./auth-module";
import bookDetailsModule from "./book-details-module";
import authorDetailsModule from "./author-details-module";

export default createStore({
	modules: {
		auth: authModule,
		bookDetails: bookDetailsModule,
		authorDetails: authorDetailsModule,
	},
});
