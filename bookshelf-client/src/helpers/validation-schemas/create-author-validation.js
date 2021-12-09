import * as Yup from "yup";
import {
	fieldRequired,
	minLength,
	dateNotFuture,
	sizeTooBig,
	maxLength,
} from "@/helpers/validation-messages";

export const createAuthorSchema = Yup.object({
	profilePhoto: Yup.mixed()
		.nullable()
		.test("file-size-limit", sizeTooBig(2), (file) => {
			if (file?.size > 2 * 1024 * 1024) {
				return false;
			}
			return true;
		}),

	firstName: Yup.string().min(2, minLength(2)).required(fieldRequired()),

	lastName: Yup.string().min(2, minLength(2)).required(fieldRequired()),

	dateOfBirth: Yup.date().nullable().max(new Date(), dateNotFuture()),

	dateOfDeath: Yup.date().nullable().max(new Date(), dateNotFuture()),

	biography: Yup.string()
		.min(10, minLength(10))
		.max(2000, maxLength(2000))
		.required(fieldRequired()),
});
