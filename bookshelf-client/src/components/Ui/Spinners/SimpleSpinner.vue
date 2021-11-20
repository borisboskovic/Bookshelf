<template>
	<div class="simple-spinner__container">
		<i class="fa fa-spinner" :style="`font-size: ${scale ?? 1}rem`"></i>
	</div>
</template>

<script>
	export default {
		props: {
			scale: Number,
		},
	};
</script>

<style lang="scss" scoped>
	@import "./SimpleSpinner";
</style>
