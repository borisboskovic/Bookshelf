<template>
	<div>
		<div class="card-container">
			<LoadingSpinner v-if="isSubmitting" />
			<h3>Register</h3>
			<Form :validation-schema="registerFormSchema" @submit="registerSubmitHandler">
				<div class="form-split-container">
					<div>
						<div class="form-control pt-2">
							<ImageUploader
								name="profilePhoto"
								label="Profile photo"
								@wrongformat="notifyIncorrectFormat"
							/>
						</div>
						<div class="form-control pt-1">
							<InputField label="First name" name="firstName" type="text" />
						</div>
						<div class="form-control pt-1">
							<InputField label="Last name" name="lastName" type="text" />
						</div>
						<div class="form-control pt-1">
							<InputField label="Email" name="email" type="email" />
						</div>
					</div>
					<div>
						<div class="form-control pt-1">
							<InputField label="Password" name="password" type="password" />
						</div>
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
					<ButtonComponent :size="'large'" :disabled="isSubmitting">
						Register
					</ButtonComponent>
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
	import { computed } from "vue";
	import { useStore } from "vuex";
	import { Form } from "vee-validate";
	import { registerFormSchema } from "@/helpers/validaton-schemas/register-form-validation";
	import { showErrorToast } from "@/services/notifications/notification-templates";
	import InputField from "@/components/Ui/Validation/InputField";
	import ButtonComponent from "@/components/Ui/Buttons/ButtonComponent.vue";
	import LoadingSpinner from "@/components/Ui/Spinners/LoadingSpinner.vue";
	import ImageUploader from "@/components/Ui/Validation/ImageUploader.vue";

	export default {
		components: {
			InputField,
			ButtonComponent,
			Form,
			LoadingSpinner,
			ImageUploader,
		},
		props: {
			setForm: Function,
		},
		setup: (props) => {
			const { dispatch, state } = useStore();

			const isSubmitting = computed(() => state.auth.isSubmitting);

			const navigateLogin = () => {
				props.setForm("login");
			};

			const registerSubmitHandler = (values) => {
				dispatch("auth/register", values);
			};

			const notifyIncorrectFormat = () => {
				showErrorToast("Error", "File type is not supported");
			};

			return {
				navigateLogin,
				registerSubmitHandler,
				registerFormSchema,
				isSubmitting,
				notifyIncorrectFormat,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./FormStyles";
	@import "./RegisterForm";
</style>
