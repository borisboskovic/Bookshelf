// TODO: Fix yup imports: https://vee-validate.logaretm.com/v4/tutorials/best-practices
import * as Yup from "yup";
import { fieldRequired, invalidEmailAddress } from "@/helpers/validation-messages";

export const resetFormSchema = Yup.object({
	email: Yup.string().email(invalidEmailAddress()).required(fieldRequired()),
});
