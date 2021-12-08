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
			<div class="review-item__toggle-comments" v-if="commentsCount > 0 && !commentsShown">
				<span @click="toggleShowComments">Show {{ commentsCount }} comments</span>
			</div>
			<div class="review-item__toggle-comments" v-if="commentsCount > 0 && commentsShown">
				<span @click="toggleShowComments">Hide comments</span>
				<CommentsList :comments="review.comments" />
			</div>
		</div>
	</div>
</template>

<script>
	import { ref, computed } from "vue";
	import CommentsList from "@/components/Reviews/CommentsList.vue";
	import FallbackImage from "@/components/Ui/Imaging/FallbackImage.vue";

	export default {
		props: {
			review: Object,
			allowEdit: Boolean,
			allowDelete: Boolean,
		},
		components: {
			CommentsList,
			FallbackImage,
		},
		setup: (props) => {
			const commentsShown = ref(false);

			const postedOn = computed(() =>
				new Date(props.review.postedOn).toLocaleDateString("sr")
			);

			const commentsCount = computed(() => props.review.comments.length);

			const toggleShowComments = () => {
				commentsShown.value = !commentsShown.value;
			};

			return {
				postedOn,
				commentsCount,
				commentsShown,
				toggleShowComments,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./ReviewItem";
</style>
