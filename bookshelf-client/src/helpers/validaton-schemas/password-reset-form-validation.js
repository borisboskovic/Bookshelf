import * as Yup from "yup";
// TODO: Fix yup imports: https://vee-validate.logaretm.com/v4/tutorials/best-practices

export const resetFormSchema = Yup.object({
	email: Yup.string()
		.email("E-mail adresa nije validna")
		.required("* Obavezno polje"),
});
