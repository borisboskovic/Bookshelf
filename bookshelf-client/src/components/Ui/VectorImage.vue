<template>
	<svg
		:width="width"
		:height="height"
		viewBox="0 0 50 40"
		fill="none"
		xmlns="http://www.w3.org/2000/svg"
	>
		<path class="path" :d="path" :fill="fill" />
	</svg>
</template>

<script>
	export default {
		props: {
			path: {
				type: String,
				required: true,
			},
			fill: {
				type: String,
				required: false,
				default: "#000000",
			},
			width: {
				type: Number,
				required: false,
			},
			height: {
				type: Number,
				required: false,
			},
		},
	};
</script>

<style lang="scss" scoped>
	@import "./VectorImage.scss";
</style>
