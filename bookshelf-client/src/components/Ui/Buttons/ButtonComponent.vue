<template>
	<input type="button" :value="text" />
</template>

<script>
	export default {
		props: {
			text: String,
		},
	};
</script>

<style lang="scss" scoped>
	@import "./ButtonComponent";
</style>
