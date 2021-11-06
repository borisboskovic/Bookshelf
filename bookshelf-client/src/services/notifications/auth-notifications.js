import { showOptionPopup } from "./notification-templates";
import store from "@/store";

export const notifyRegisterSuccess = () => {
	const text = "Registration sucsessfull";
	const actionName = "Back to login page";

	const action = () => {
		store.dispatch("auth/setFormType", "login");
	};

	showOptionPopup(text, actionName, action);
};
