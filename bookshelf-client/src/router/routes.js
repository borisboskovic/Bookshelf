import {
	AuthorDetailsPage,
	AuthPage,
	BookDetailsPage,
	HomePage,
	MyBooksPage,
	NotFoundPage,
	CreateAuthorPage,
	CreateBookPage,
	CreateBookIssuePage,
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
	{
		name: "CreateAuthor",
		path: "/create-author",
		component: CreateAuthorPage,
	},
	{
		name: "CreateBook",
		path: "/create-book",
		component: CreateBookPage,
	},
	{
		name: "CreateBookIssue",
		path: "/create-book-issue",
		component: CreateBookIssuePage,
	},
];

export default routes;
