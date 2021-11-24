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
			state.wantToRead = payload.wantToRead.map((e) => ({
				...e,
				list: "wantToRead",
			}));
			state.currentlyReading = payload.currentlyReading.map((e) => ({
				...e,
				list: "currentlyReading",
			}));
			state.read = payload.read.map((e) => ({
				...e,
				list: "read",
			}));
		},

		REMOVE_FROM_LIST: (state, payload) => {
			state[payload.list] = state[payload.list].filter((e) => e.bookIssueId !== payload.id);
		},

		MOVE_TO_LIST: (state, payload) => {
			const item = state[payload.previousList].find((e) => e.bookIssueId === payload.id);
			state[payload.previousList] = state[payload.previousList].filter((e) => {
				return e.bookIssueId !== payload.id;
			});
			state[payload.nextList] = state[payload.nextList].concat({
				...item,
				list: payload.nextList,
			});
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

		addToList: async ({ commit }, payload) => {
			await axios.post("BookList/AddBook", payload).then(() => {
				if (payload.previousList) {
					commit("MOVE_TO_LIST", payload);
				}
			});
		},

		removeFromList: async ({ commit }, payload) => {
			await axios
				.delete("BookList/RemoveBook", {
					data: payload,
				})
				.then(() => {
					commit("REMOVE_FROM_LIST", payload);
				});
		},
	},
};
