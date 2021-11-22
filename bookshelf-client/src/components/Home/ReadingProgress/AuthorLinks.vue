<template>
	<div class="authors-container">
		<div>by&nbsp;</div>
		<div class="author" v-for="author in authors" :key="author.authorId">
			<router-link :to="`/author-details/${author.authorId}`">{{ author.name }}</router-link>
			<span>,&nbsp;</span>
		</div>
	</div>
</template>

<script>
	export default {
		props: {
			authors: Array,
		},
	};
</script>

<style lang="scss" scoped>
	@import "./AuthorLinks";
</style>
