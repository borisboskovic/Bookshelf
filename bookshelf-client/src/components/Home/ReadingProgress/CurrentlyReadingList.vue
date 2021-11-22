<template>
	<div class="list-container">
		<div class="caption">Currently reading</div>
		<div class="currently-reading-list">
			<CurrentlyReadingItem
				v-for="book in items"
				:key="book.bookIssueId"
				:id="book.bookIssueId"
				:title="book.title"
				:authors="book.authors"
				:image="book.imageUrl"
				:totalPages="book.totalPages"
				:pagesRead="book.pagesRead"
				:dispatch="dispatch"
			/>
		</div>
		<div class="actions-container">
			<span class="action-control">View All</span>
			<span class="dot-separator">&middot;</span>
			<span class="action-control">Add a book</span>
		</div>
	</div>
</template>

<script>
	import { computed, onMounted } from "vue";
	import { useStore } from "vuex";
	import CurrentlyReadingItem from "./CurrentlyReadingItem.vue";

	export default {
		components: {
			CurrentlyReadingItem,
		},
		setup: () => {
			const { state, dispatch } = useStore();
			const items = computed(() => state.currentlyReading.items);

			onMounted(() => {
				dispatch("currentlyReading/fetchItems");
			});

			return {
				items,
				dispatch,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./CurrentlyReadingList";
</style>
