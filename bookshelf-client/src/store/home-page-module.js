import { axios } from "@/config";

export default {
	namespaced: true,

	state: {
		authors: [],
		bookIssues: [],
	},

	mutations: {
		SET_ITEMS: (state, payload) => {
			state.authors = payload.authors ?? [];
			state.bookIssues = payload.bookIssues ?? [];
		},
	},

	actions: {
		fetchItems: async ({ commit }) => {
			await axios.get("/Home").then((response) => {
				commit("SET_ITEMS", response.data);
			});
		},
	},
};
