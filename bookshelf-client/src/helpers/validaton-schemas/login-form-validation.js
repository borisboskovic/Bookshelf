// TODO: Fix yup imports: https://vee-validate.logaretm.com/v4/tutorials/best-practices
import * as Yup from "yup";
import { fieldRequired, invalidEmailAddress, minLength } from "../validation-messages";

export const loginFormSchema = Yup.object({
	email: Yup.string().email(invalidEmailAddress()).required(fieldRequired()),

	password: Yup.string().min(8, minLength(8)).required(fieldRequired()),
});
