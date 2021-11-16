<template>
	<div class="author-details-page__main-container">
		<LoadingSpinner v-if="isLoading" />
		<NotFoundComponent v-if="notFoundError" />
		<AuthorDetailsComponent v-if="author" :author="author" />
	</div>
</template>

<script>
	import { onMounted, onUnmounted, computed } from "vue";
	import { useRoute } from "vue-router";
	import { useStore } from "vuex";
	import LoadingSpinner from "@/components/Ui/Spinners/LoadingSpinner.vue";
	import NotFoundComponent from "@/components/NotFound/NotFoundComponent.vue";
	import AuthorDetailsComponent from "@/components/Author/AuthorDetailsComponent.vue";

	export default {
		components: {
			LoadingSpinner,
			NotFoundComponent,
			AuthorDetailsComponent,
		},
		setup: () => {
			const { state, dispatch } = useStore();
			const { params } = useRoute();
			const isLoading = computed(() => state.authorDetails.isLoading);
			const notFoundError = computed(() => state.authorDetails.notFoundError);
			const author = computed(() => state.authorDetails.author);

			onMounted(() => {
				document.title = "Author details";

				if (params?.id) {
					dispatch("authorDetails/fetchDetails", params.id);
				}
			});

			onUnmounted(() => {
				dispatch("authorDetails/clearAuthor");
			});

			return {
				isLoading,
				notFoundError,
				author,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./AuthorDetailspage";
</style>
