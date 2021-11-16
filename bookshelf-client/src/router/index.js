import { createWebHistory, createRouter } from "vue-router";
import routes from "./routes";
import store from "@/store";

const router = createRouter({
	history: createWebHistory(),
	routes: routes,
});

router.beforeEach((to, from, next) => {
	const isLoggedIn = store.state.auth.isLoggedIn;
	if (!isLoggedIn && to.name !== "Auth") {
		next({ name: "Auth" });
	} else if (isLoggedIn && to.name === "Auth") {
		next({ name: "Home" });
	} else {
		next();
	}
});

export default router;
