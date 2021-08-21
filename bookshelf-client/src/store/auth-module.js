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
