<template>
	<div class="auth-outside-container">
		<div class="auth-container-background"></div>
		<RegisterForm v-if="formType === 'register'" :setForm="setFormType" />
		<div v-else class="auth-inside-container">
			<div class="stripe-container"></div>
			<div class="image-container">
				<img :src="booksImage" alt="Books on shelf" />
			</div>
			<div class="form-container">
				<LoginForm v-if="formType === 'login'" :setForm="setFormType" />
				<ForgotPassword v-if="formType === 'reset'" :setForm="setFormType" />
			</div>
		</div>
	</div>
</template>

<script>
	import { onMounted, onUpdated, computed } from "vue";
	import { useStore } from "vuex";
	import LoginForm from "./LogInForm.vue";
	import RegisterForm from "./RegisterForm.vue";
	import ForgotPassword from "./ForgotPassword.vue";
	// import booksImage from "@/assets/images/rasters/pile-of-books.jpg";
	import booksImage from "@/assets/images/rasters/watch-gc320662af_1920.jpg";

	export default {
		components: {
			LoginForm,
			RegisterForm,
			ForgotPassword,
		},
		setup: () => {
			const { state, dispatch } = useStore();
			const formType = computed(() => state.auth.formType);

			const setFormType = (nextVal) => {
				dispatch("auth/setFormType", nextVal);
			};

			const updateDocumentTitle = () => {
				switch (formType.value) {
					case "login":
						document.title = "BookShelf - Log in";
						break;

					case "register":
						document.title = "BookShelf - Register";
						break;

					case "reset":
						document.title = "BookShelf - Reset Password";
						break;

					default:
						document.title = "BookShelf";
						break;
				}
			};

			onMounted(updateDocumentTitle);

			onUpdated(updateDocumentTitle);

			return {
				formType,
				setFormType,
				booksImage,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./AuthFormContainer";
</style>
