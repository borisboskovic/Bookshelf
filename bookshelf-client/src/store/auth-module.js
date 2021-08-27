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
		LOGIN_TEST: (state) => {
			state.isLoggedIn = !state.isLoggedIn;
		},
	},

	actions: {
		login: ({ commit }, payload) => {},
		loginTest: ({ commit }) => {
			commit("LOGIN_TEST");
		},
	},
};
