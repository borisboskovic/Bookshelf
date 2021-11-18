<template>
	<div :class="className">
		By: &nbsp;
		<router-link
			class="link-element"
			v-for="author in authors"
			:key="author"
			:to="`/author-details/${author.id}`"
		>
			{{ author.name }}
		</router-link>
	</div>
</template>

<script>
	export default {
		props: {
			authors: Array,
			className: String,
		},
	};
</script>

<style lang="scss" scoped>
	@import "./AuthorLinks";
</style>
