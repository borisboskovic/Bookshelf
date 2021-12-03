import { createStore } from "vuex";
import authModule from "./auth-module";
import bookDetailsModule from "./book-details-module";
import authorDetailsModule from "./author-details-module";
import currentlyReadingModule from "./currently-reading-module";
import bookListsModule from "./book-lists-module";
import homePageModule from "./home-page-module";

export default createStore({
	modules: {
		auth: authModule,
		bookDetails: bookDetailsModule,
		authorDetails: authorDetailsModule,
		currentlyReading: currentlyReadingModule,
		bookLists: bookListsModule,
		homePage: homePageModule,
	},
});
