<template>
	<div class="title-accent">Reviews</div>
	<div v-if="isFetchingReviews" class="loading-spinner-container">
		<SimpleSpinner :scale="3" />
	</div>
	<div v-else class="reviews__main-container">
		<div v-if="myReview" class="reviews__my-review">
			<div class="reviews__my-review__title">My Review</div>
			<ReviewItem :review="myReview" :allowEdit="true" :allowDelete="true" />
		</div>
		<div v-else class="reviews__my-review">
			<ReviewForm @submit-review="submitReviewHandler" />
		</div>

		<div class="reviews__other-reviews">
			<ReviewItem v-for="r in otherReviews" :key="r.authorId" :review="r" />
		</div>
	</div>
</template>

<script>
	import { ref, computed, onMounted } from "vue";
	import { useStore } from "vuex";
	import SimpleSpinner from "@/components/Ui/Spinners/SimpleSpinner.vue";
	import ReviewItem from "@/components/Reviews/ReviewItem.vue";
	import ReviewForm from "@/components/Reviews/ReviewForm.vue";

	export default {
		props: {
			bookIssueId: Number,
		},
		components: {
			SimpleSpinner,
			ReviewItem,
			ReviewForm,
		},
		setup: (props) => {
			const { state, dispatch } = useStore();
			const isFetchingReviews = ref(true);

			const myReview = computed(() => state.bookDetails.myReview);
			const otherReviews = computed(() => state.bookDetails.otherReviews);

			onMounted(() => {
				dispatch("bookDetails/fetchReviews", props.bookIssueId).finally(() => {
					isFetchingReviews.value = false;
				});
			});

			const submitReviewHandler = (text) => {
				dispatch("bookDetails/postReview", {
					bookIssueId: props.bookIssueId,
					content: text,
				});
			};

			return {
				isFetchingReviews,
				myReview,
				otherReviews,
				submitReviewHandler,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./BookReviews";
</style>
