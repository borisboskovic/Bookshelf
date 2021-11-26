<template>
	<Form :validation-schema="reviewPostSchema" @submit="submitReviewHandler">
		<div class="reviews__form-control">
			<VeeTextArea name="postContent" />
		</div>
		<ButtonComponent size="large">Post</ButtonComponent>
	</Form>
</template>

<script>
	import { Form } from "vee-validate";
	import VeeTextArea from "@/components/Ui/Validation/VeeTextArea.vue";
	import ButtonComponent from "@/components/Ui/Buttons/ButtonComponent.vue";
	import { reviewPostSchema } from "@/helpers/validation-schemas/review-post-validation";

	export default {
		components: {
			VeeTextArea,
			ButtonComponent,
			Form,
		},
		emits: ["submit-review"],
		setup: (_, context) => {
			const submitReviewHandler = (values) => {
				context.emit("submit-review", values.postContent);
			};

			return {
				reviewPostSchema,
				submitReviewHandler,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./ReviewForm";
</style>
