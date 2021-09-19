<template>
	<button :class="buttonClasses">
		<slot />
	</button>
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
		setup: (props) => {
			const buttonClasses = `button-element ${props.size}`;
			return {
				buttonClasses,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./ButtonComponent";
</style>
