<template>
	<div v-if="bookIssues.length > 0" class="list-wrapper">
		<div class="list-title">Recently Added Book Issues</div>
		<BasicBookList :items="bookIssues" />
	</div>
	<div v-if="authors.length > 0" class="list-wrapper">
		<div class="list-title">Recently Added Authors</div>
		<ResponsiveGridContainer>
			<RecentAuthor v-for="author in authors" :key="author.id" :author="author" />
		</ResponsiveGridContainer>
	</div>
</template>

<script>
	import { computed } from "vue";
	import { useStore } from "vuex";
	import ResponsiveGridContainer from "@/components/Ui/Containers/ResponsiveGridContainer.vue";
	import RecentAuthor from "./RecentAuthor.vue";
	import BasicBookList from "@/components/Books/BasicBookList.vue";

	export default {
		components: {
			ResponsiveGridContainer,
			RecentAuthor,
			BasicBookList,
		},
		setup: () => {
			const { state } = useStore();

			const authors = computed(() => state.homePage.authors);
			const bookIssues = computed(() => state.homePage.bookIssues);

			return {
				authors,
				bookIssues,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./RecentItems";
</style>
