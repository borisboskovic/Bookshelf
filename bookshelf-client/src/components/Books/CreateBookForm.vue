<template>
	<div class="card-container">
		<LoadingSpinner v-if="isSubmitting" />
		<h3>Add book</h3>
		<Form @submit="submitHandler">
			<div class="button-container">
				<ButtonComponent :size="'large'" :disabled="isSubmitting"> Submit </ButtonComponent>
			</div>
		</Form>
	</div>
</template>

<script>
	import { ref } from "vue";
	import { Form } from "vee-validate";
	import LoadingSpinner from "@/components/Ui/Spinners/LoadingSpinner.vue";
	import ButtonComponent from "../../components/Ui/Buttons/ButtonComponent.vue";

	export default {
		components: {
			LoadingSpinner,
			Form,
			ButtonComponent,
		},
		setup: () => {
			const isSubmitting = ref(false);

			const submitHandler = (values) => {
				console.log("Values", values);
			};

			return {
				isSubmitting,
				submitHandler,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "@/assets/style/form-styles";
	@import "./CreateBookForm";
</style>
