import { axios } from "@/config";

export default {
	namespaced: true,

	state: {
		isLoading: true,
		notFoundError: false,
		book: null,
	},

	mutations: {
		SET_LOADING: (state, payload) => {
			state.isLoading = payload;
		},

		SET_NOT_FOUND: (state, payload) => {
			state.notFoundError = payload;
		},

		SET_BOOK: (state, payload) => {
			state.book = payload;
		},

		CLEAR_BOOK: (state) => {
			state.book = null;
		},
	},

	actions: {
		fetchDetails: ({ commit }, payload) => {
			commit("SET_LOADING", true);
			commit("SET_NOT_FOUND", false);
			axios
				.get(`BookDetails?bookIssueId=${payload}`)
				.then((response) => {
					commit("SET_BOOK", response.data);
				})
				.catch(() => {
					commit("SET_NOT_FOUND", true);
				})
				.finally(() => {
					commit("SET_LOADING", false);
				});
		},

		clearBook: ({ commit }) => {
			commit("CLEAR_BOOK");
		},
	},
};
