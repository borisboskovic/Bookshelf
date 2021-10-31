import { axios } from "@/config";

export default {
	namespaced: true,

	state: {
		isLoggedIn: false,
		email: null,
		fullName: null,
		roles: [],
	},

	mutations: {
		LOGIN: (state, payload) => {},
	},

	actions: {
		login: ({ commit }, payload) => {
			axios
				.post("User/Authenticate", payload)
				.then((response) => {
					console.log("Response", response.data);
				})
				.catch((error) => {
					// console.log(error.response?.data);
				});
		},
	},
};
