import Swal from "sweetalert2";
import colors from "@/assets/strings/colors";
import toastr from "toastr";

const toastOptions = {
	closeButton: true,
	preventDuplicates: true,
	progressBar: true,
	timeOut: 5000,
};

export const showOptionPopup = (text, actionName, callback) => {
	Swal.fire({
		title: "Success",
		text: text,
		confirmButtonColor: colors.accent,
		confirmButtonText: actionName,
		icon: "success",
	}).then((result) => {
		if (result.isConfirmed && callback) {
			callback();
		}
	});
};

export const showSuccessToast = (title, description) => {
	toastr.success(description, title, toastOptions);
};

export const showInfoToast = (title, description) => {
	toastr.info(description, title, toastOptions);
};

export const showWarningToast = (title, description) => {
	toastr.warning(description, title, toastOptions);
};

export const showErrorToast = (title, description) => {
	toastr.error(description, title, toastOptions);
};
