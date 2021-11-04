<template>
	<div>
		<div class="card-container">
			<h3>Register</h3>
			<Form :validation-schema="registerFormSchema" @submit="registerSubmitHandler">
				<div class="form-split-container">
					<div>
						<div class="form-control pt-2">
							<InputField label="First name" name="firstName" type="text" />
						</div>
						<div class="form-control pt-1">
							<InputField label="Last name" name="lastName" type="text" />
						</div>
						<div class="form-control pt-1">
							<InputField label="Email" name="email" type="email" />
						</div>
						<div class="form-control pt-1">
							<InputField label="Password" name="password" type="password" />
						</div>
					</div>
					<div>
						<div class="form-control pt-2">
							<InputField
								label="Repeat password"
								name="repeatPassword"
								type="password"
							/>
						</div>
						<div class="form-control pt-1">
							<InputField label="Phone number" name="phoneNumber" type="text" />
						</div>
						<div class="form-control pt-1">
							<InputField label="Date of birth" name="dob" type="date" />
						</div>
					</div>
				</div>
				<div class="button-container">
					<ButtonComponent :size="'large'"> Register </ButtonComponent>
				</div>
			</Form>
		</div>
		<div class="subtle-label">
			Already have an account?
			<span @click="navigateLogin">Login</span>
		</div>
	</div>
</template>

<script>
	import { Form } from "vee-validate";
	import { registerFormSchema } from "@/helpers/validaton-schemas/register-form-validation";
	import InputField from "@/components/Ui/Validation/InputField";
	import ButtonComponent from "@/components/Ui/Buttons/ButtonComponent.vue";

	export default {
		components: {
			InputField,
			ButtonComponent,
			Form,
		},
		props: {
			setForm: Function,
		},
		setup: (props) => {
			const navigateLogin = () => {
				props.setForm("login");
			};

			const registerSubmitHandler = (values) => {
				console.log("Register", values);
			};

			return {
				navigateLogin,
				registerSubmitHandler,
				registerFormSchema,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./FormStyles";
	@import "./RegisterForm";
</style>
