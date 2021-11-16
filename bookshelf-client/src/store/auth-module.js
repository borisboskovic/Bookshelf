import { axios } from "@/config";
import jwtDecode from "jwt-decode";
import router from "@/router";
import { notifyRegisterSuccess } from "@/services/notifications/auth-notifications";

let timeout;

export default {
	namespaced: true,

	state: {
		formType: "login", // login, register, reset
		isSubmitting: false,
		isLoggedIn: false,
		id: null,
		email: null,
		fullName: null,
		avatar: null,
		roles: [],
	},

	mutations: {
		LOGIN: (state, payload) => {
			state.isLoggedIn = true;
			state.id = payload.sub;
			state.email = payload.email;
			state.fullName = payload.name;
			state.roles = Array.of(payload.roles);
		},
		LOGOUT: (state) => {
			state.isLoggedIn = false;
			state.id = null;
			state.email = null;
			state.fullName = null;
			state.avatar = null;
			state.roles = [];
		},
		SET_USER_INFO: (state, payload) => {
			state.avatar = payload.avatar;
		},
		SET_FORM_TYPE: (state, payload) => {
			state.formType = payload;
		},
		SET_SUBMITTING: (state, payload) => {
			state.isSubmitting = payload;
		},
	},

	actions: {
		setFormType: ({ commit }, payload) => {
			commit("SET_FORM_TYPE", payload);
		},
		login: ({ commit }, payload) => {
			commit("SET_SUBMITTING", true);
			axios
				.post("User/Authenticate", payload)
				.then((response) => {
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
								router.push({ name: "Auth" });
								commit("LOGOUT");
							}, tokenLifetime);
							commit("LOGIN", tokenPayload);

							router.push({ name: "Home" });
						}
					}
				})
				.finally(() => {
					commit("SET_SUBMITTING", false);
				});
		},
		fetchUserInfo: ({ commit }) => {
			axios.get("User/Info").then((response) => {
				commit("SET_USER_INFO", response.data);
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
						router.push({ name: "Auth" });
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
		register: ({ commit }, payload) => {
			commit("SET_SUBMITTING", true);

			const formData = new FormData();
			formData.append("firstName", payload.firstName);
			formData.append("lastName", payload.lastName);
			formData.append("email", payload.email);
			formData.append("dateOfBirth", payload.dob);
			formData.append("password", payload.password);
			formData.append("profilePhoto", payload.profilePhoto);

			axios
				.post("User/Register", formData)
				.then(() => {
					notifyRegisterSuccess();
				})
				.finally(() => {
					commit("SET_SUBMITTING", false);
				});
		},
	},
};
