<template>
	<MyBookListItem v-for="book in items" :key="book.bookIssueId" :title="book.title" />
</template>

<script>
	import MyBookListItem from "@/components/MyBooks/MyBookListItem";

	export default {
		props: {
			items: Array,
		},
		components: {
			MyBookListItem,
		},
	};
</script>

<style lang="scss" scoped>
	@import "./MyBookList";
</style>
