import * as Yup from "yup";

export const loginFormSchema = Yup.object({
	email: Yup.string()
		.email("E-mail adresa nije validna")
		.required("* Obavezno polje"),

	password: Yup.string()
		.min(8, "Minimalna dužina je 8 karaktera")
		.required("* Obavezno polje"),
});
