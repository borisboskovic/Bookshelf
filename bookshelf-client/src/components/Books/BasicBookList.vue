<template>
	<ResponsiveGridContainer>
		<BasicBookListItem
			v-for="book in items"
			:key="book.bookIssueId"
			:id="book.bookIssueId"
			:title="book.title"
			:image="book.imageUrl"
			:rating="book.rating"
			:reviewsCount="book.reviewsCount"
		/>
	</ResponsiveGridContainer>
</template>

<script>
	import ResponsiveGridContainer from "@/components/Ui/Containers/ResponsiveGridContainer.vue";
	import BasicBookListItem from "@/components/Books/BasicBookListItem.vue";

	export default {
		props: {
			items: Array,
		},
		components: {
			ResponsiveGridContainer,
			BasicBookListItem,
		},
	};
</script>

<style lang="scss" scoped>
	@import "./BasicBookList";
</style>
