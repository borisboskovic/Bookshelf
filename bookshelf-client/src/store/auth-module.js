export default {
	namespaced: true,

	state: {
		isLoggedIn: true,
		email: null,
		fullName: null,
		roles: [],
	},

	mutations: {
		LOGIN: (state, payload) => {},
	},

	actions: {
		login: ({ commit }, payload) => {},
	},
};
