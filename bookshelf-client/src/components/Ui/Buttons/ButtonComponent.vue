<template>
	<input type="button" :value="text" />
</template>

<script>
	export default {
		props: {
			text: String,
			size: {
				type: String,
				required: true,
				validator: (value) => {
					return ["small", "normal", "large"].indexOf(value) !== -1;
				},
			},
		},
	};
</script>

<style lang="scss" scoped>
	@import "./ButtonComponent";
</style>
