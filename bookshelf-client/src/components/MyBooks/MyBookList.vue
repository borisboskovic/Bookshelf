<template>
	<div class="book-list__container">
		<div class="book-list__title">My Books</div>
		<ResponsiveGridContainer3>
			<MyBookListItem
				v-for="book in items"
				:key="book.bookIssueId"
				:id="book.bookIssueId"
				:title="book.title"
				:image="book.imageUrl"
				:authors="book.authors"
				:ratings="book.ratings"
				:addedOn="book.addedOn"
				:list="book.list"
			/>
		</ResponsiveGridContainer3>
	</div>
</template>

<script>
	import MyBookListItem from "@/components/MyBooks/MyBookListItem";
	import ResponsiveGridContainer3 from "@/components/Ui/Containers/ResponsiveGridContainer3.vue";

	export default {
		props: {
			items: Array,
		},
		components: {
			MyBookListItem,
			ResponsiveGridContainer3,
		},
	};
</script>

<style lang="scss" scoped>
	@import "./MyBookList";
</style>
