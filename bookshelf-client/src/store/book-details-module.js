import { axios } from "@/config";

export default {
	namespaced: true,

	state: {
		isLoading: true,
		isSubmittingRating: false,
		notFoundError: false,
		book: null,
		myReview: null,
		otherReviews: [],
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

		UPDATE_LIST: (state, payload) => {
			state.book.list = payload;
		},

		CLEAR_BOOK: (state) => {
			state.book = null;
		},

		SET_REVIEWS: (state, payload) => {
			state.myReview = payload.myReview;
			state.otherReviews = payload.otherReviews;
		},

		SET_MY_REVIEW: (state, payload) => {
			state.myReview = {
				...payload,
				comments: [],
			};
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
				.post("BookReview/RateBook", payload)
				.then((response) => {
					commit("UPDATE_RATINGS", response.data);
				})
				.finally(() => {
					commit("SET_SUBMITTING_RATING", false);
				});
		},

		addToList: async ({ commit }, payload) => {
			await axios.post("BookList/AddBook", payload).then(() => {
				commit("UPDATE_LIST", payload.nextList);
			});
		},

		removeFromList: async ({ commit }, payload) => {
			await axios
				.delete("BookList/RemoveBook", {
					data: payload,
				})
				.then(() => {
					commit("UPDATE_LIST", null);
				});
		},

		clearBook: ({ commit }) => {
			commit("CLEAR_BOOK");
		},

		fetchReviews: async ({ commit }, payload) => {
			await axios.get(`BookReview?bookIssueId=${payload}`).then((response) => {
				commit("SET_REVIEWS", response.data);
			});
		},

		postReview: async ({ commit }, payload) => {
			await axios.post("BookReview/AddReview", payload).then((response) => {
				commit("SET_MY_REVIEW", response.data);
			});
		},
	},
};
