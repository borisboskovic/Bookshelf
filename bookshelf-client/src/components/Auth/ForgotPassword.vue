<template>
	<div>
		<div class="card-container">
			<h3>Reset your password</h3>
			<Form :validation-schema="resetFormSchema" @submit="resetSubmitHandler">
				<div class="form-control pt-2">
					<InputField label="Email adress" name="email" type="email" />
				</div>
				<div class="button-container">
					<ButtonComponent :size="'large'"> Reset </ButtonComponent>
				</div>
			</Form>
		</div>
		<div class="action-label subtle-label" @click="navigateLogin">Back to Login page</div>
	</div>
	<div class="additional-controls subtle-label">
		Dont have an account?
		<span @click="navigateRegister">Register</span>
	</div>
</template>

<script>
	import { Form } from "vee-validate";
	import { resetFormSchema } from "@/helpers/validation-schemas/password-reset-form-validation";
	import InputField from "@/components/Ui/Validation/InputField.vue";
	import ButtonComponent from "@/components/Ui/Buttons/ButtonComponent";

	export default {
		props: {
			setForm: Function,
		},
		components: {
			Form,
			InputField,
			ButtonComponent,
		},
		setup: (props) => {
			const navigateRegister = () => {
				props.setForm("register");
			};

			const navigateLogin = () => {
				props.setForm("login");
			};

			const resetSubmitHandler = (values) => {
				console.log("Submit", values);
			};

			return {
				navigateRegister,
				navigateLogin,
				resetFormSchema,
				resetSubmitHandler,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "@/assets/style/form-styles";
</style>
