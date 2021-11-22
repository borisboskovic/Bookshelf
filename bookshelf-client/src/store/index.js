import { createStore } from "vuex";
import authModule from "./auth-module";
import bookDetailsModule from "./book-details-module";
import authorDetailsModule from "./author-details-module";
import currentlyReadingModule from "./currently-reading-module";

export default createStore({
	modules: {
		auth: authModule,
		bookDetails: bookDetailsModule,
		authorDetails: authorDetailsModule,
		currentlyReading: currentlyReadingModule,
	},
});
