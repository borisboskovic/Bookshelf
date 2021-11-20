import { axios } from "@/config";

export default {
	namespaced: true,

	state: {
		isLoading: true,
		isSubmittingRating: false,
		notFoundError: false,
		book: null,
	},

	mutations: {
		SET_LOADING: (state, payload) => {
			state.isLoading = payload;
		},

		SET_SUBMITTING_RATING: (state, payload) => {
			state.isSubmittingRating = payload;
		},

		SET_NOT_FOUND: (state, payload) => {
			state.notFoundError = payload;
		},

		SET_BOOK: (state, payload) => {
			state.book = payload;
		},

		UPDATE_RATINGS: (state, payload) => {
			state.book.ratings = payload;
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

		postRating: ({ commit }, payload) => {
			commit("SET_SUBMITTING_RATING", true);
			axios
				.post("Review/RateBook", payload)
				.then((response) => {
					commit("UPDATE_RATINGS", response.data);
				})
				.finally(() => {
					commit("SET_SUBMITTING_RATING", false);
				});
		},

		clearBook: ({ commit }) => {
			commit("CLEAR_BOOK");
		},
	},
};
