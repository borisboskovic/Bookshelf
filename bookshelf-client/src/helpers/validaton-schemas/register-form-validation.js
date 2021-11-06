// TODO: Fix yup imports: https://vee-validate.logaretm.com/v4/tutorials/best-practices
import * as Yup from "yup";
import {
	fieldRequired,
	invalidEmailAddress,
	minLength,
	passwordMismatch,
	minDate,
	dateNotFuture,
} from "@/helpers/validation-messages";

export const registerFormSchema = Yup.object({
	firstName: Yup.string().min(2, minLength(2)).required(fieldRequired()),

	lastName: Yup.string().min(2, minLength(2)).required(fieldRequired()),

	email: Yup.string().email(invalidEmailAddress()).required(fieldRequired()),

	password: Yup.string().min(8, minLength(8)).required(fieldRequired()),

	repeatPassword: Yup.string()
		.oneOf([Yup.ref("password"), null], passwordMismatch())
		.required(fieldRequired()),

	dob: Yup.date()
		.required(fieldRequired())
		.min(new Date(1900, 0, 1), minDate())
		.max(new Date(), dateNotFuture()),
});
