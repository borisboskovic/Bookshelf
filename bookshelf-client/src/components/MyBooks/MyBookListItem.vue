<template>{{ title }}</template>

<script>
	export default {
		props: {
			title: String,
		},
	};
</script>

<style lang="scss" scoped>
	@import "./MyBookListItem";
</style>
