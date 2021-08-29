import { createWebHistory, createRouter } from "vue-router";
import AuthPage from "../pages/AuthPage";
import HomePage from "../pages/HomePage";
import MyBooksPage from "../pages/MyBooksPage";

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
];

const router = createRouter({
	history: createWebHistory(),
	routes: routes,
});

export default router;
