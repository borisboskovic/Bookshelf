<template>
	<div class="home-page-container">
		<div class="sidebar">
			<CurrentlyReadingList />
		</div>
		<div class="main-container">
			<LoadingSpinner v-if="isLoading" />
			<RecentItems v-else-if="hasFetchedSuccessfully" />
		</div>
	</div>
</template>

<script>
	import { ref, onMounted } from "vue";
	import { useStore } from "vuex";
	import CurrentlyReadingList from "@/components/Home/ReadingProgress/CurrentlyReadingList.vue";
	import LoadingSpinner from "@/components/Ui/Spinners/LoadingSpinner.vue";
	import RecentItems from "@/components/Home/RecentItems/RecentItems.vue";

	export default {
		components: {
			CurrentlyReadingList,
			LoadingSpinner,
			RecentItems,
		},
		setup: () => {
			const isLoading = ref(true);
			const hasFetchedSuccessfully = ref(false);
			const { dispatch } = useStore();

			onMounted(() => {
				document.title = "BookShelf - Home Page";
				dispatch("homePage/fetchItems")
					.then(() => {
						hasFetchedSuccessfully.value = true;
					})
					.finally(() => {
						isLoading.value = false;
					});
			});

			return {
				isLoading,
				hasFetchedSuccessfully,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./HomePage";
</style>
