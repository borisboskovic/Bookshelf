import {
	AuthorDetailsPage,
	AuthPage,
	BookDetailsPage,
	HomePage,
	MyBooksPage,
	NotFoundPage,
} from "@/pages";

const routes = [
	{
		name: "Home",
		path: "/",
		component: HomePage,
	},
	{
		name: "Auth",
		path: "/auth",
		component: AuthPage,
	},
	{
		name: "MyBooks",
		path: "/my-books",
		component: MyBooksPage,
	},
	{
		name: "Author",
		path: "/author-details/:id",
		component: AuthorDetailsPage,
	},
	{
		name: "BookDetails",
		path: "/book-details/:id",
		component: BookDetailsPage,
	},
	{
		name: "NotFound",
		path: "/:catchAll(.*)",
		component: NotFoundPage,
	},
];

export default routes;
