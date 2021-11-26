// TODO: Fix yup imports: https://vee-validate.logaretm.com/v4/tutorials/best-practices
import * as Yup from "yup";
import { fieldRequired, maxLength } from "@/helpers/validation-messages";

export const reviewPostSchema = Yup.object({
	postContent: Yup.string().required(fieldRequired()).max(2000, maxLength(2000)),
});
