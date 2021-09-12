<template>
	<div class="auth-outside-container">
		<div class="auth-container-background"></div>
		<div class="auth-inside-container">
			<div class="stripe-container"></div>
			<div class="image-container">
				<img :src="booksImage" alt="Books on shelf" />
			</div>
			<LoginForm v-if="isLoginForm" />
			<div class="form-switch-controls">
				<div v-if="!isLoginForm" class="log-in-siwtch">
					<span>Already have an account?</span>
					&nbsp;
					<span @click="setIsLoginForm(true)">Log in</span>
				</div>
				<div v-else class="register-switch">
					<span>Don't have an account?</span>
					&nbsp;
					<span @click="setIsLoginForm(false)">Register</span>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
	import { ref, onMounted, onUpdated } from "vue";
	import LoginForm from "./LogInForm.vue";
	import booksImage from "@/assets/images/rasters/books-on-shelf.jpg";

	export default {
		components: {
			LoginForm,
		},
		setup: () => {
			const isLoginForm = ref(true);

			const setIsLoginForm = (nextVal) => {
				isLoginForm.value = nextVal;
			};

			const updateDocumentTitle = () => {
				document.title = isLoginForm.value ? "BookShelf - Log in" : "BookShelf - Register";
			};

			onMounted(updateDocumentTitle);

			onUpdated(updateDocumentTitle);

			return {
				isLoginForm,
				setIsLoginForm,
				booksImage,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./AuthFormContainer";
</style>
