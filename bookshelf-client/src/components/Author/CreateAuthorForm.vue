<template>
	<div class="card-container">
		<LoadingSpinner v-if="isSubmitting" />
		<h3>Add author</h3>
		<Form :validation-schema="createAuthorSchema" @submit="submitHandler">
			<div class="form-split-container">
				<div>
					<div class="form-control pt-1">
						<InputField label="First name" name="firstName" type="text" />
					</div>
					<div class="form-control pt-1">
						<InputField label="Last name" name="lastName" type="text" />
					</div>
					<div class="form-control pt-1">
						<InputField label="Date of birth" name="dateOfBirth" type="date" />
					</div>
					<div class="form-control pt-1">
						<InputField label="Date of death" name="dateOfDeath" type="date" />
					</div>
					<div class="form-control pt-1">
						<InputField label="Place of birth" name="placeOfBirth" type="text" />
					</div>
				</div>
				<div>
					<div class="form-control pt-1">
						<ImageUploader
							name="authorPhoto"
							label="Author photo"
							@wrongformat="notifyIncorrectFormat"
						/>
					</div>
					<div class="form-control pt-1 flex-grow">
						<label for="biography" class="input-label">Biography</label>
						<VeeTextArea name="biography" className="text-area__main-form" />
					</div>
				</div>
			</div>
			<div class="button-container">
				<ButtonComponent :size="'large'" :disabled="isSubmitting"> Submit </ButtonComponent>
			</div>
		</Form>
	</div>
</template>

<script>
	import { ref } from "vue";
	import router from "@/router";
	import axios from "@/config/axios";
	import { createAuthorSchema } from "@/helpers/validation-schemas/create-author-validation";
	import { showErrorToast } from "@/services/notifications/notification-templates";
	import { Form } from "vee-validate";
	import LoadingSpinner from "@/components/Ui/Spinners/LoadingSpinner.vue";
	import ImageUploader from "@/components/Ui/Validation/ImageUploader.vue";
	import InputField from "@/components/Ui/Validation/InputField";
	import VeeTextArea from "@/components/Ui/Validation/VeeTextArea";
	import ButtonComponent from "@/components/Ui/Buttons/ButtonComponent.vue";

	export default {
		components: {
			LoadingSpinner,
			Form,
			ImageUploader,
			InputField,
			VeeTextArea,
			ButtonComponent,
		},
		setup: () => {
			const isSubmitting = ref(false);

			const notifyIncorrectFormat = () => {
				showErrorToast("Error", "File type is not supported");
			};

			const submitHandler = (values) => {
				isSubmitting.value = true;

				const formData = new FormData();
				formData.append("firstName", values.firstName);
				formData.append("lastName", values.lastName);
				formData.append("authorPhoto", values.authorPhoto);
				formData.append("biography", values.biography);
				formData.append("dateOfBirth", values.dateOfBirth);
				formData.append("dateOfDeath", values.dateOfDeath);
				formData.append("placeOfBirth", values.placeOfBirth);

				axios
					.post("AuthorDetails", formData)
					.then((response) => {
						router.push(`/author-details/${response.data.id}`);
					})
					.finally(() => {
						isSubmitting.value = false;
					});
			};

			return {
				isSubmitting,
				notifyIncorrectFormat,
				submitHandler,
				createAuthorSchema,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "@/assets/style/form-styles";
	@import "./CreateAuthorForm";
</style>
