import axios from "axios";
import store from "../store";
import { BASE_URL } from "@/config/constants";

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

		// TODO: Notify
		console.log("AXIOS error.response", error.response);
		console.log("AXIOS error.response.data", error.response.data);

		return Promise.reject(error);
	}
);

export default instance;
