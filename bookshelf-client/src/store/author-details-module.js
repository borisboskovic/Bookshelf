import { axios } from "@/config";

export default {
	namespaced: true,

	state: {
		isLoading: true,
		notFoundError: false,
		author: null,
	},

	mutations: {
		SET_LOADING: (state, payload) => {
			state.isLoading = payload;
		},

		SET_NOT_FOUND: (state) => {
			state.notFoundError = true;
		},

		SET_AUTHOR: (state, payload) => {
			state.author = payload;
		},

		CLEAR_AUTHOR: (state) => {
			state.author = null;
		},
	},

	actions: {
		fetchDetails: ({ commit }, payload) => {
			axios
				.get(`AuthorDetails?authorId=${payload}`)
				.then((response) => {
					console.log("Response", response.data);
					commit("SET_AUTHOR", response.data);
				})
				.catch(() => {
					commit("SET_NOT_FOUND");
				})
				.finally(() => {
					commit("SET_LOADING", false);
				});
		},

		clearAuthor: ({ commit }) => {
			commit("CLEAR_AUTHOR");
		},
	},
};
