import axios from "axios";
import store from "@/store";
import { BASE_URL } from "@/config/constants";
import { showErrorToast } from "@/services/notifications/notification-templates";

const instance = axios.create({
	baseURL: BASE_URL,
	headers: {
		Accept: "application/json",
		"Content-Type": "application/json",
	},
});

axios.defaults.baseURL = BASE_URL;

instance.interceptors.request.use(
	async (config) => {
		const accessToken = localStorage.getItem("token");
		if (accessToken) {
			config.headers["Authorization"] = `Bearer ${accessToken}`;
		}
		return config;
	},
	(error) => {
		Promise.reject(error);
	}
);

instance.interceptors.response.use(
	(response) => {
		return response;
	},
	async function (error) {
		if (error && error.response && error.response.status === 401) {
			store.dispatch("auth/logout");
		}

		const errors = error.response?.data?.errors;
		if (errors) {
			const errorEntries = Object.entries(errors);
			errorEntries.forEach((e) => showErrorToast(e[0], e[1]));
		}

		return Promise.reject(error);
	}
);

export default instance;
