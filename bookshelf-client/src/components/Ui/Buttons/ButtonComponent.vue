<template>
	<button :class="buttonClasses" :disabled="disabled">
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
			disabled: Boolean,
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
