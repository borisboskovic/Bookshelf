import AuthPage from "@/pages/AuthPage";
import HomePage from "@/pages/HomePage";
import MyBooksPage from "@/pages/MyBooksPage";
import AuthorPage from "@/pages/AuthorPage";
import BookPage from "@/pages/BookPage";

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
		path: "/author/:id",
		component: AuthorPage,
	},
	{
		name: "Book",
		path: "/book/:id",
		component: BookPage,
	},
];

export default routes;
