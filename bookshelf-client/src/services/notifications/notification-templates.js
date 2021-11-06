import Swal from "sweetalert2";
import colors from "../../assets/strings/colors";
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

export const showErrorToast = (title, description) => {
	toastr.error(description, title, toastOptions);
};
