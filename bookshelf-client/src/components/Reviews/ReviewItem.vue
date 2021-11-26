<template>
	<div class="review-item__container">
		<div class="review-item__image-container">
			<FallbackImage :source="review.authorImage" />
		</div>
		<div class="review-item__inner-container">
			<div class="review-item__name">{{ review.authorName }}</div>
			<div class="review-item__content">
				{{ review.content }}
			</div>
			<div class="review-item__posted-date">{{ postedOn }}</div>
			<div class="review-item__admin-controls">
				<div v-if="allowEdit">Edit</div>
				<div v-if="allowDelete">Delete</div>
			</div>
		</div>
	</div>
</template>

<script>
	import { computed } from "vue";
	import FallbackImage from "@/components/Ui/Imaging/FallbackImage.vue";

	export default {
		props: {
			review: Object,
			allowEdit: Boolean,
			allowDelete: Boolean,
		},
		components: {
			FallbackImage,
		},
		setup: (props) => {
			const postedOn = computed(() =>
				new Date(props.review.postedOn).toLocaleDateString("sr")
			);

			console.log("Review", props.review);

			return {
				postedOn,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./ReviewItem";
</style>
