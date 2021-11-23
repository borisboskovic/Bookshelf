import { axios } from "@/config";

export default {
	namespaced: true,

	state: {
		isLoading: false,
		wantToRead: [],
		currentlyReading: [],
		read: [],
	},

	mutations: {
		SET_LOADING: (state, payload) => {
			state.isLoading = payload;
		},

		SET_ITEMS: (state, payload) => {
			state.wantToRead = payload.wantToRead;
			state.currentlyReading = payload.currentlyReading;
			state.read = payload.read;
		},
	},

	actions: {
		fetchAll: ({ commit }) => {
			commit("SET_LOADING", true);
			axios
				.get("BookList")
				.then((response) => {
					commit("SET_ITEMS", response.data);
				})
				.finally(() => {
					commit("SET_LOADING", false);
				});
		},
	},
};
