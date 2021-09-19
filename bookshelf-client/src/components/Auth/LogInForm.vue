<template>
	<div>
		<div class="card-container">
			<h3>Log in</h3>

			<Form :validation-schema="loginFormSchema" @submit="loginSubmitHandler">
				<div class="form-control">
					<InputField label="Email adress" name="email" type="email" />
				</div>
				<div class="form-control">
					<InputField label="Password" name="password" type="password" />
				</div>
				<div class="button-container">
					<ButtonComponent :size="'large'">
						Log In
					</ButtonComponent>
				</div>
			</Form>
		</div>
		<div class="forgot-password subtle-label" @click="navigateResetPassword">
			Forgot your password?
		</div>
	</div>
	<div class="additional-controls subtle-label">
		Dont have an account?
		<span @click="navigateRegister">Register</span>
	</div>
</template>

<script>
	import { Form } from "vee-validate";
	import { loginFormSchema } from "@/helpers/validaton-schemas/login-form-validation";
	import InputField from "@/components/Ui/Validation/InputField.vue";
	import ButtonComponent from "@/components/Ui/Buttons/ButtonComponent";

	export default {
		props: {
			setForm: Function,
		},
		components: {
			ButtonComponent,
			Form,
			InputField,
		},
		setup: (props) => {
			const navigateResetPassword = () => {
				props.setForm("reset");
			};

			const navigateRegister = () => {
				props.setForm("register");
			};

			const loginSubmitHandler = (values) => {
				console.log("Submit", values);
			};

			return {
				navigateResetPassword,
				navigateRegister,
				loginFormSchema,
				loginSubmitHandler,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./LoginForm";
</style>
