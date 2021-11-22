import { axios } from "@/config";

export default {
	namespaced: true,

	state: {
		isFetching: false,
		items: [],
	},

	mutations: {
		SET_IS_FETCHING: (state, payload) => {
			state.isFetching = payload;
		},

		SET_ITEMS: (state, payload) => {
			state.items = payload;
		},

		UPDATE_PROGRESS: (state, payload) => {
			state.items = state.items.map((e) => {
				return e.bookIssueId !== payload.bookIssueId
					? e
					: { ...e, pagesRead: payload.pagesRead };
			});
		},

		REMOVE_ITEM: (state, payload) => {
			state.items = state.items.filter((e) => e.bookIssueId !== payload);
		},
	},

	actions: {
		fetchItems: ({ commit }) => {
			commit("SET_IS_FETCHING", true);
			axios
				.get("BookList/CurrentlyReading")
				.then((response) => {
					commit("SET_ITEMS", response.data);
				})
				.finally(() => {
					commit("SET_IS_FETCHING", false);
				});
		},

		updateProgress: ({ commit }, payload) => {
			axios.put("BookList/UpdateProgress", payload).then((response) => {
				commit("UPDATE_PROGRESS", response.data);
			});
		},

		finishBook: ({ commit }, payload) => {
			axios.put(`BookList/FinishBook?bookIssueId=${payload}`).then(() => {
				commit("REMOVE_ITEM", payload);
			});
		},
	},
};
