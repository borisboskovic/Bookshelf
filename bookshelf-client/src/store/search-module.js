import axios from "@/config/axios";

export default {
	namespaced: true,

	state: {
		searchTerm: null,
		mainSearch: null,
	},

	mutations: {
		CLEAR_SEARCH: (state) => {
			state.searchTerm = null;
			state.mainSearch = null;
		},

		SET_RESULTS: (state, payload) => {
			state.mainSearch = payload;
		},
	},

	actions: {
		fetchResults: ({ commit }, payload) => {
			axios.get(`Home/Search/searchString=${payload}`).then((response) => {
				commit("SET_RESULTS", response.data);
			});
		},
	},
};
