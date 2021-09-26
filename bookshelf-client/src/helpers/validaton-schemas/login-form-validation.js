import * as Yup from "yup";
// TODO: Fix yup imports: https://vee-validate.logaretm.com/v4/tutorials/best-practices

export const loginFormSchema = Yup.object({
	email: Yup.string()
		.email("E-mail adresa nije validna")
		.required("* Obavezno polje"),

	password: Yup.string()
		.min(8, "Minimalna dužina je 8 karaktera")
		.required("* Obavezno polje"),
});
