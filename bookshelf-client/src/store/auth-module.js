import { axios } from "@/config";
import jwtDecode from "jwt-decode";
import router from "@/router";

let timeout;

export default {
	namespaced: true,

	state: {
		isLoggedIn: false,
		email: null,
		fullName: null,
		roles: [],
	},

	mutations: {
		LOGIN: (state, payload) => {
			state.isLoggedIn = true;
			state.email = payload.email;
			state.fullName = payload.name;
			state.roles = Array.of(payload.roles);
		},
		LOGOUT: (state) => {
			state.isLoggedIn = false;
			state.email = null;
			state.fullName = null;
			state.roles = [];
		},
	},

	actions: {
		login: ({ commit }, payload) => {
			axios.post("User/Authenticate", payload).then((response) => {
				const token = response.data?.token;
				if (token) {
					const tokenPayload = jwtDecode(token);
					if (tokenPayload) {
						const tokenLifetime = tokenPayload.exp * 1000 - new Date().getTime();

						// Don't log in if token expires shortly after
						if (tokenLifetime < 60000) {
							return;
						}

						localStorage.setItem("token", token);

						clearTimeout(timeout);
						timeout = setTimeout(() => {
							localStorage.removeItem("token");
							commit("LOGOUT");
						}, tokenLifetime);

						commit("LOGIN", tokenPayload);

						router.push({ name: "Home" });
					}
				}
			});
		},
		persistedLogin: ({ commit }) => {
			const token = localStorage.getItem("token");
			if (token) {
				const tokenPayload = jwtDecode(token);
				if (tokenPayload) {
					const tokenLifetime = tokenPayload.exp * 1000 - new Date().getTime();

					// Don't log in if token expires shortly after
					if (tokenLifetime < 60000) {
						return;
					}

					clearTimeout(timeout);
					timeout = setTimeout(() => {
						localStorage.removeItem("token");
						commit("LOGOUT");
					}, tokenLifetime);

					commit("LOGIN", tokenPayload);
				}
			}
		},
		logout: ({ commit }) => {
			localStorage.removeItem("token");
			router.push({ name: "Auth" });
			commit("LOGOUT");
		},
	},
};
