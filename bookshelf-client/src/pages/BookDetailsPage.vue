<template>
	<div class="book-details-page__main-container">
		<LoadingSpinner v-if="isLoading" />
		<NotFoundComponent v-if="notFoundError" />
		<BookDetailsComponent v-if="book" :book="book" />
	</div>
</template>

<script>
	import { onMounted, onUnmounted, computed } from "vue";
	import { useRoute } from "vue-router";
	import { useStore } from "vuex";
	import LoadingSpinner from "@/components/Ui/Spinners/LoadingSpinner.vue";
	import NotFoundComponent from "@/components/NotFound/NotFoundComponent.vue";
	import BookDetailsComponent from "@/components/Books/BookDetailsComponent.vue";

	export default {
		components: {
			BookDetailsComponent,
			LoadingSpinner,
			NotFoundComponent,
		},
		setup: () => {
			const { state, dispatch } = useStore();
			const { params } = useRoute();
			const isLoading = computed(() => state.bookDetails.isLoading);
			const notFoundError = computed(() => state.bookDetails.notFoundError);
			const book = computed(() => state.bookDetails.book);

			onMounted(() => {
				document.title = "Book details";

				if (params?.id) {
					dispatch("bookDetails/fetchDetails", params.id);
				}
			});

			onUnmounted(() => {
				dispatch("bookDetails/clearBook");
			});

			return {
				isLoading,
				notFoundError,
				book,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./BookDetailsPage";
</style>
