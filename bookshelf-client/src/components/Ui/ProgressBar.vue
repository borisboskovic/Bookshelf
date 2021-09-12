<template>
	<div class="progress-bar">
		<div class="progress-done" :style="width"></div>
	</div>
</template>

<script>
	import { computed } from "vue";

	export default {
		props: {
			total: {
				type: Number,
				required: true,
			},
			done: {
				type: Number,
				required: true,
			},
		},
		setup: (props) => {
			const width = computed(() => {
				const percentage = (props.done / props.total).toFixed(2);
				return {
					width: percentage * 100 + "%",
				};
			});

			return {
				width,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./ProgressBar";
</style>
