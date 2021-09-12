<template>
	<div class="list-container">
		<div class="caption">Currently reading</div>
		<div class="currently-reading-list">
			<CurrentlyReadingItem
				v-for="book in items"
				:key="book.id"
				:id="book.id"
				:title="book.title"
				:authors="book.authors"
				:image="book.image"
				:totalPages="book.totalPages"
				:pagesRead="book.pagesRead"
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
	import CurrentlyReadingItem from "./CurrentlyReadingItem.vue";
	import { items } from "@/temp/currently-reading";
	export default {
		components: {
			CurrentlyReadingItem,
		},
		setup: () => {
			return {
				items,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./CurrentlyReadingList";
</style>
