import { createWebHistory, createRouter } from "vue-router";
import AuthPage from "../pages/AuthPage";
import HomePage from "../pages/HomePage";

const routes = [
	{
		name: "Auth",
		path: "/auth",
		component: AuthPage,
	},
	{
		name: "Home",
		path: "/",
		component: HomePage,
	},
];

const router = createRouter({
	history: createWebHistory(),
	routes: routes,
});

export default router;
